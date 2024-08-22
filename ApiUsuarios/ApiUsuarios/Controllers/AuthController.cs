﻿using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Text.Json;
using Newtonsoft.Json;

namespace ApiUsuarios.Controllers
{
    [System.Web.Mvc.RoutePrefix("auth")]
    public class AuthController : Controller
    {
        public class formaLogin
        {
            public string User { get; set; }
            public string Pass { get; set; }
        }

        public class TipoToken
        {
            public string token { get; set; }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("token")]
        public dynamic Token([FromBody] formaLogin login)
        {
            try
            {
                if (login == null || string.IsNullOrEmpty(login.User) || string.IsNullOrEmpty(login.Pass))
                {
                    return Json("Solicitud inválida");
                }
                else
                {
                    if (!InicioDeSesion(login.User,login.Pass))
                    {
                        return Json("Solicitud inválida");
                    }
                    else
                    {
                        string tokenString = GenerarToken(login.User);
                        return Json(tokenString);
                    }
                }
            }
            catch
            {
                return Json("Solicitud inválida");
            }
        }
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("prueba")]
        public dynamic prueba()
        {
            return "si";
        }
       
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("testToken")]
        public dynamic TestToken([FromBody] TipoToken pedido)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                var comprobar = tokenHandler.ValidateToken(pedido.token, ParametrosDeValidacionDelToken(), out securityToken);
                return Json(true);
            }
            catch
            {
                return Json(false);
            }   
        }

        public bool InicioDeSesion(string nombredecuenta, string contraseña)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT nombreDeCuenta,contrasena FROM Login WHERE nombreDeCuenta=@nombredecuenta", conn);
                command.Parameters.AddWithValue("@nombredecuenta", nombredecuenta);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["nombreDeCuenta"].ToString().Equals(nombredecuenta) && reader["contrasena"].ToString().Equals(contraseña))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }  
        }

        public string GenerarToken(string user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("af431f66a2b44ddf1c8ee210f366d921"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Sub, user),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(issuer: "InfiniSV", audience: "usuario", claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private TokenValidationParameters ParametrosDeValidacionDelToken()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = "InfiniSV",
                ValidAudience = "usuario",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("af431f66a2b44ddf1c8ee210f366d921"))
            };
        }

        //pruebas de la lógica
        public dynamic PRToken(formaLogin login)
        {
            try
            {
                if (login == null || string.IsNullOrEmpty(login.User) || string.IsNullOrEmpty(login.Pass))
                {
                    return JsonConvert.SerializeObject("Solicitud inválida");
                }
                else
                {
                    if (!InicioDeSesion(login.User, login.Pass))
                    {
                        return JsonConvert.SerializeObject("Solicitud inválida");
                    }
                    else
                    {
                        string tokenString = GenerarToken(login.User);
                        return JsonConvert.SerializeObject(tokenString);
                    }
                }
            }
            catch
            {
                return JsonConvert.SerializeObject("Solicitud inválida");
            }
        }

        public dynamic PRTestToken(TipoToken pedido)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                var comprobar = tokenHandler.ValidateToken(pedido.token, ParametrosDeValidacionDelToken(), out securityToken);
                return JsonConvert.SerializeObject(true);
            }
            catch
            {
                return JsonConvert.SerializeObject(false);
            }
        }
    }
}