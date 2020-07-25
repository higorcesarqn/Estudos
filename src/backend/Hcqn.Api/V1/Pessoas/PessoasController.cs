using Hcqn.Core.Bus;
using Hcqn.Core.Bus.Abstractions;
using Hcqn.Core.Models;
using Hcqn.Core.Notifications;
using Hcqn.Core.Notifications.Abstractions;
using Hcqn.Core.PagedList;
using Hcqn.Core.UnitOfWork.Abstractions;
using Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands.Dtos;
using Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands.PessoaFisicaCommands.Create;
using Hcqn.Api.Controllers;
using Hcqn.Api.Infrastructure.Filters;
using Hcqn.Api.V1.Pessoas.Models;
using Hcqn.Domain.AggregatesModel.PessoaAggregate;
using Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hcqn.Api.V1.Pessoas
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PessoasController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notifications"></param>
        public PessoasController(INotifications notifications) : base(notifications)
        {
        }

        /// <summary>
        /// Lista as pessoas cadastradas
        /// Roles: [pessoas-listar,pessoas-editar]
        /// </summary>
        /// <param name="pessoaRepository"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IPagedList<Pessoa>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        [Authorize(Roles = "pessoas-listar,pessoas-editar")]

        public async Task<IActionResult> Get([FromServices] IPessoaRepository pessoaRepository,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 20)
        {
            var list = await pessoaRepository.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize);

            return Response(list);
        }


        /// <summary>
        /// Detalha uma Pessoa.
        /// Roles: [pessoas-listar,pessoas-editar,pessoas-detalhar]
        /// </summary>
        /// <param name="pessoaRepository"></param>
        /// <param name="idPessoa"></param>
        /// <returns></returns>
        [HttpGet("{idPessoa}")]
        [ProducesResponseType(typeof(Pessoa), 200)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        [Authorize(Roles = "pessoas-listar,pessoas-editar,pessoas-detalhar")]
        public async Task<IActionResult> Get([FromServices] IPessoaRepository pessoaRepository, Guid idPessoa)
        {
            var pessoa = await pessoaRepository.GetDetails(idPessoa);
            return Response(pessoa);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="adicionarPessoaModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        [Authorize(Roles = "pessoas-criar")]
        public async Task<IActionResult> Post([FromServices] IMediatorHandler bus, [FromBody] AdicionarPessoaModel adicionarPessoaModel)
        {
            var telefones = adicionarPessoaModel.Telefones
                    .Select(x => new TelefoneDto(x.IdTipoTelefone, x.DDD, x.Numero));

            var documentos = adicionarPessoaModel.Documentos.Select(x => new DocumentoDto(x.IdTipoDocumento, x.Valor));

            var enderecos = adicionarPessoaModel.Enderecos
                    .Select(x => new EnderecoDto(x.IdTipoEndereco, x.CEP, x.Uf, x.Cidade, x.Bairro, x.Logradouro, x.Numero, x.Complemento));

            var comando = new CreateNewPessoaFisicaCommand(
                adicionarPessoaModel.Nome,
                adicionarPessoaModel.Email,
                adicionarPessoaModel.Nascimento,
                adicionarPessoaModel.Deficiente,
                adicionarPessoaModel.Estuda,
                adicionarPessoaModel.IdEscolaridade,
                telefones.ToList(),
                documentos.ToList(),
                enderecos.ToList());

            await bus.SendCommand(comando);

            return ResponseCreated($"api/v1/pessoas/{comando.Id}", new { comando.Id, comando.Nome });
        }

        /// <summary>
        /// Lista os Tipos de Escolaridades. 
        /// Roles: [pessoas-editar, pessoas-adicionar]
        /// </summary>
        /// <returns></returns>
        [HttpGet("tipos-escolaridades")]
        [ProducesResponseType(typeof(IEnumerable<Escolaridade>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        [Authorize(Roles = "pessoas-adicionar,pessoas-editar")]
        [ResponseCache(CacheProfileName = "Default1hr")]
        public IActionResult GetEscolaridade()
        {
            var all = Enumeration.GetAll<Escolaridade>();

            return Response(all);
        }

        /// <summary>
        /// Lista os Tipos de Pessoas. 
        /// Roles: [pessoas-editar, pessoas-adicionar]
        /// </summary>
        /// <returns></returns>
        [HttpGet("tipos-pessoas")]
        [ProducesResponseType(typeof(IEnumerable<TipoPessoa>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        [Authorize(Roles = "pessoas-adicionar,pessoas-editar")]
        [ResponseCache(CacheProfileName = "Default1hr")]
        public IActionResult GetTipoPessoa()
        {
            var all = Enumeration.GetAll<TipoPessoa>();

            return Response(all);
        }

        /// <summary>
        /// Lista os Tipos de Telefones. 
        /// Roles: [pessoas-editar, pessoas-adicionar]
        /// </summary>
        /// <returns></returns>
        [HttpGet("tipos-telefones")]
        [ProducesResponseType(typeof(IEnumerable<TipoTelefone>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        [Authorize(Roles = "pessoas-adicionar,pessoas-editar")]
        [ResponseCache(CacheProfileName = "Default1hr")]
        public IActionResult GetTipoTelefone()
        {
            var all = Enumeration.GetAll<TipoTelefone>();

            return Response(all);
        }

        /// <summary>
        /// Lista os Tipos de Endereços. 
        /// Roles: [pessoas-editar, pessoas-adicionar]
        /// </summary>
        /// <returns></returns>
        [HttpGet("tipos-enderecos")]
        [ProducesResponseType(typeof(IEnumerable<TipoEndereco>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        [Authorize(Roles = "pessoas-adicionar,pessoas-editar")]
        [ResponseCache(CacheProfileName = "Default1hr")]
        public IActionResult GetTipoEnderecos()
        {
            var all = Enumeration.GetAll<TipoEndereco>();

            return Response(all);
        }

        /// <summary>
        /// Lista tipos de documentos a partir do id tipo pessoa
        /// Roles : [pessoas-adicionar,pessoas-editar]
        /// </summary>
        /// <param name="idTipoPessoa"></param>
        /// <returns></returns>
        [HttpGet("tipos-documentos/{idTipoPessoa}")]
        [ProducesResponseType(typeof(IEnumerable<TipoDocumento>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        [Authorize(Roles = "pessoas-adicionar, pessoas-editar")]
        [ResponseCache(CacheProfileName = "Default1hr")]
        public IActionResult GetTiposDocumentos([FromRoute] int idTipoPessoa)
        {
            var result = Enumeration.GetAll<TipoDocumento>()
                .Where(x => x.TipoPessoa.Id == idTipoPessoa);

            return Response(result);
        }
    }
}