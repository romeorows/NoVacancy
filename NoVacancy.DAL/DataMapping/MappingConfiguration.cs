using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NoVacancy.DTO;
using NoVacancy.DAL.Entities;
using NoVacancy.DTO.DataTransferObjects;

namespace DataMapping
{
    public static class MappingConfiguration
    {
        public static void CreateEstablishment()
        {
            Mapper.CreateMap<trEstablishment, DTOEstablishment>();
            Mapper.CreateMap<DTOEstablishment, trEstablishment>();

        }

        public static Toutput MapObjects<TInput, Toutput>(TInput inputObj)
        {
            return Mapper.Map<Toutput>(inputObj);
        }

        public static List<Toutput> MapObjectsList<TInput, Toutput>(List<TInput> inputObj)
        {
            var data = new List<Toutput>();
            foreach (var row in inputObj)
            {
                data.Add(Mapper.Map<Toutput>(row));
            }

            return data;
        }
    }
}
