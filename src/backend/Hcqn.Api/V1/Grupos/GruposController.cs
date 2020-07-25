﻿using Hcqn.Core.Bus.Abstractions;
using Hcqn.Core.Notifications.Abstractions;
using Hcqn.Core.UnitOfWork.Abstractions;
using Hcqn.AdministracaoUsuario.Application.Commands.RoleCommands.Create;
using Hcqn.AdministracaoUsuario.Application.Commands.RoleCommands.Update;
using Hcqn.Api.Controllers;
using Hcqn.Api.Infrastructure.Filters;
using Hcqn.Api.V1.Grupos.Models;
using Hcqn.Domain.AggregatesModel.RoleAggregate;
using Hcqn.Domain.AggregatesModel.RoleAggregate.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hcqn.Api.V1.Grupos
{
    /// <summary>
    /// Controller de Grupos
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GruposController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notifications"></param>
        public GruposController(INotifications notifications) : base(notifications) { }

        /// <summary>
        /// Lista os Grupos. Roles [grupos-detalhar,grupos-editar]
        /// </summary>
        /// <param name="roleRepository"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("listar")]
        [Authorize(Roles = "grupos-detalhar,grupos-editar")]
        [ProducesResponseType(typeof(IPagedList<RoleDetailsDto>), 201)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        public async Task<IActionResult> Get([FromServices]IRoleRepository roleRepository,
             [FromQuery] int pageIndex = 0,
             [FromQuery] int pageSize = 20)
        {
            var result = await roleRepository.GetPagedList(pageIndex: pageIndex, pageSize: pageSize);

            return Response(result);
        }

        /// <summary>
        /// Detalha um grupo
        /// </summary>
        /// <param name="roleRepository"></param>
        /// <param name="idGrupo"></param>
        /// <returns></returns>
        [HttpGet("{idGrupo}")]
        [Authorize(Roles = "grupos-detalhar,grupos-editar")]
        [ProducesResponseType(typeof(IPagedList<RoleDetailsDto>), 201)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        public async Task<IActionResult> Get([FromServices]IRoleRepository roleRepository, [FromRoute] Guid idGrupo)
        {
            var result = await roleRepository.Get(idGrupo);

            return Response(result);
        }

        /// <summary>
        /// Adiciona um novo grupo.
        /// Roles: [grupos-adicionar]
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="novoGrupoModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "grupos-adicionar")]
        [ProducesResponseType(typeof(GrupoAdicionadoModel), 201)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        public async Task<IActionResult> Post([FromServices]IMediatorHandler bus, [FromBody] NovoGrupoModel novoGrupoModel)
        {
            var comando = new CreateNewRoleCommand(novoGrupoModel.Nome, novoGrupoModel.Permissoes);

            await bus.SendCommand(comando);
            return ResponseCreated($"api/grupo/{comando.Id}", new GrupoAdicionadoModel { Id = comando.Id, Nome = comando.Name });
        }

        /// <summary>
        /// Edita um Grupo.
        /// Roles: [grupos-editar]
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="idGrupo"></param>
        /// <param name="editarGrupoModel"></param>
        /// <returns></returns>
        [HttpPut("{idGrupo}")]
        [Authorize(Roles = "grupos-editar")]
        [ProducesResponseType(typeof(GrupoAdicionadoModel), 201)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        public async Task<IActionResult> Put([FromServices]IMediatorHandler bus, Guid idGrupo, EditarGrupoModel editarGrupoModel)
        {
            var updateRoleCommand = new UpdateRoleCommand(idGrupo, editarGrupoModel.Nome, editarGrupoModel.Permissoes);

            var result = await bus.SendCommand(updateRoleCommand);

            return Response(result);
        }

        /// <summary>
        /// Lista as permissões.
        /// Roles: [grupos-adicionar, grupos-editar]
        /// </summary>
        /// <param name="permissionRepository"></param>
        /// <returns></returns>
        [HttpGet("Permissoes/Listar")]
        [Authorize(Roles = "grupos-adicionar, grupos-editar")]
        [ProducesResponseType(typeof(PermissaoDto), 200)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        public async Task<IActionResult> GetPermissoes([FromServices]IRoleRepository permissionRepository)
        {
            return Response(await permissionRepository.ListAllPermissions());
        }
    }
}
