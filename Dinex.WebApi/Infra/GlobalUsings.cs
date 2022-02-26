
global using System.Text;
global using System.ComponentModel.DataAnnotations;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;

global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Cors;

global using Microsoft.EntityFrameworkCore;

global using Microsoft.IdentityModel.Tokens;

global using Microsoft.Extensions.Options;

global using FluentValidation;
global using FluentValidation.AspNetCore;

global using AutoMapper;

global using MailKit.Net.Smtp;
global using MimeKit;

global using Dinex.WebApi.API.Models;
global using Dinex.WebApi.API.Controllers;
global using Dinex.WebApi.Entities;
global using Dinex.WebApi.Infra;
global using Dinex.WebApi.Business;
