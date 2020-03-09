﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using cqrs_Test.Application.Models.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace cqrs_Test.Presenter.Controller
{
    [AllowAnonymous]
    [ApiController]
    [Route("auah")]

    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Authenticate(Auth user)
        {
            var auth = new List<Auth>()
            {
                new Auth() { Username = "starplatinum", Password = "zawarudo" },
                new Auth() { Username = "goldexperience", Password = "requiem" }
            };

            var _user = auth.Find(x => x.Username == user.Username);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDesc = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, _user.Username),
                    new Claim(ClaimTypes.Sid, _user.Password)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ini secret key nya harus panjang gaboleh pendek")), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDesc);

            var tokenResponse = new
            {
                token = tokenHandler.WriteToken(token),
                user = _user.Username
            };

            return Ok(tokenResponse);
        }
    }
}
