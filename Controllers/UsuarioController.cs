using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crud_back_end.Data;
using crud_back_end.Models;
using crud_back_end.DTOs;
using crud_back_end.Repositories.Interfaces;

namespace crud_back_end.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase {
        private readonly IUsuarioRepositorie _usuarioRepositorie;
        public UsuarioController(IUsuarioRepositorie usuarioRepositorie) => _usuarioRepositorie = usuarioRepositorie;

        [HttpGet]
        public async Task<ActionResult<Usuario>> GetUsuarios()
        {
            try 
            {
                var listaUsuarios = await _usuarioRepositorie.GetUsuariosAsync();
                return Ok(listaUsuarios);
            }
            catch (Exception ex )
            {
                return NotFound(ex.Message);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<Usuario>> Create(CreateUsuarioDTO createUserDTO)
        {
            try
            {
                var usuario = await _usuarioRepositorie.CreateAsync(createUserDTO);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
    }
}