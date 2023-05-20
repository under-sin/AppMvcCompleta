using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevIO.App.Data;
using DevIO.App.ViewModels;
using DevIO.Business.Interfaces;
using AutoMapper;
using DevIO.Business.Models;

namespace DevIO.App.Controllers
{
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _forncedorRepository;
        private readonly IMapper _mapper;

        public FornecedoresController(
            IFornecedorRepository fornecedorRepository,
            IMapper mapper)
        {
            _forncedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            // o repository vai retornar a model fornecedor, porem, a nossa view espera uma viewmodel
            // por isso é necessário usar o mapper para coverter
            return View(_mapper.Map<IEnumerable<FornecedorViewModel>>(await _forncedorRepository.ObterTodos()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return View(fornecedorViewModel);

            // aqui usamos o mapper para fazer o contrário, pegar um viewmodel e mapear para a model
            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _forncedorRepository.Adicionar(fornecedor);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _forncedorRepository.Atualizar(fornecedor);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null) return NotFound();

            return View(fornecedorViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null) return NotFound();

            await _forncedorRepository.Remover(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _forncedorRepository.ObterFornecedorEndereco(id));
        }

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _forncedorRepository.ObterFornecedorProdutosEndereco(id));
        }
    }
}
