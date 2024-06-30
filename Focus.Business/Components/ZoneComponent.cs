//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Focus.Business.Interface;
//using Focus.Business.Models;
//using Focus.Domain.Entities;

//namespace Focus.Business.Components
//{
//    public class ZoneComponent : IZoneComponent
//    {
//        private readonly IApplicationDbContext _context;

//        public ZoneComponent(IApplicationDbContext context)
//        {
//            _context = context;
//        }
//        public void SaveData(ZoneDto zoneDto)
//        {

//            var zone = new Zone
//            {
//                City = zoneDto.City,
//                Name = zoneDto.Name

//            };
            
//            _context.Zones.Add(zone);
//            _context.SaveChanges();
//        }

//        public List<ZoneDto> ViewList()
//        {
//            var zones=_context.Zones.ToList();
//            if (zones.Count>0)
//            {
//                var zoneList = new List<ZoneDto>();
//                foreach (var zone in zones)
//                {
//                    var z = new ZoneDto
//                    {
//                        City = zone.City,
//                        Name = zone.Name

//                    };
//                    zoneList.Add(z);
//                }

//                return zoneList;
                
//            }
//            return null;

//        }



//    }
//}
