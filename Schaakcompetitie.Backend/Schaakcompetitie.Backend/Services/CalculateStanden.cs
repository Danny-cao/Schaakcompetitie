using System.Collections.Generic;
using System.Linq;
using Schaakcompetitie.Backend.DAL.DTO;
using Schaakcompetitie.Backend.DAL.Models;

namespace Schaakcompetitie.Backend.Services
{
    public class CalculateStanden
    {
        private List<Partij> _partijen;
        private List<Speler> _spelers;
        
        public CalculateStanden(List<Partij> partijen, List<Speler> spelers)
        {
            _spelers = spelers;
            _partijen = partijen;
        }

        public List<StandDTO> Generate()
        {
            List<StandDTO> standen = new List<StandDTO>();
            
            foreach (var speler in _spelers)
            {

                StandDTO stand = new StandDTO();
                
                stand.Speler = speler.Naam;
                stand.Partij = 0;
                stand.Gewonnen = 0;
                stand.Gelijk = 0;
                stand.Verloren = 0;
                stand.Score = 0;

                foreach (var partij in _partijen)
                {
                    if (speler == partij.Witspeler || speler == partij.Zwartspeler)
                    {
                        stand.Partij++;
                        
                        if ( partij.Uitslag == 3)
                        {
                            stand.Gelijk += 1;
                            stand.Score += 0.5f;
                        }

                        if (speler == partij.Witspeler && partij.Uitslag == 1 || speler == partij.Zwartspeler && partij.Uitslag == 2)
                        {
                            stand.Gewonnen += 1;
                            stand.Score += 1;
                        }

                        if ((speler == partij.Witspeler && partij.Uitslag == 2) || (speler == partij.Zwartspeler && partij.Uitslag == 1))
                        {
                            stand.Verloren += 1;
                        }
                    }
                }
                
                standen.Add(stand);
            }

            return standen.OrderByDescending(s => s.Score).ToList();
        }
    }
}