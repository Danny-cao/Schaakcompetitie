using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Schaakcompetitie.Frontend.DAL.DataMappers;
using Schaakcompetitie.Frontend.DAL.Models;
using Schaakcompetitie.Frontend.EventListeners.Commands;
using Schaakcompetitie.Frontend.Models;

namespace Schaakcompetitie.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _schaakapiUrl;
        private readonly IDataMapper<Stand, long> _standDataMapper;
        private readonly IDataMapper<Partij, long> _partijDataMapper;

        public HomeController(IDataMapper<Stand, long> standDataMapper, IDataMapper<Partij, long> partijDataMapper)
        {
            _standDataMapper = standDataMapper;
            _partijDataMapper = partijDataMapper;
            _schaakapiUrl = "http://schaakapiservice-service";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Stand> standen = _standDataMapper.FindAll();
            
            return View(standen);
        }
        
        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(PartijViewModel partij)
        {
            try
            {
                Partij newpartij = new Partij()
                {
                    Witspeler = partij.Witspeler,
                    Zwartspeler = partij.Zwartspeler,
                    Uitslag = partij.Uitslag
                };

                VoegPartijToeCommand partijCommand = new VoegPartijToeCommand()
                {
                    partij = newpartij
                };
                
                await _schaakapiUrl.AppendPathSegment("partij").PostJsonAsync(partijCommand).ReceiveJson<PartijViewModel>();

                _partijDataMapper.Insert(newpartij);
                
                return RedirectToAction("Index");
            }
            catch(FlurlHttpException e) {
                return RedirectToAction("Index");    
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}