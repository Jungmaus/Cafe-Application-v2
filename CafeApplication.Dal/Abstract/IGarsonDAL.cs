using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApplication.Entities.Concrate;

namespace CafeApplication.DAL.Abstract
{
    interface IGarsonDAL
    {
        int Login(string username, string pass);
        Garsonlar GetGarson(int garsonID);
        List<Garsonlar> GetGarsonList();
        void AddGarson(Garsonlar g);
        void DeleteGarson(int garsonID);
        void UpdateGarson(Garsonlar g);
        string GetGarsonIsim(int garsonID);
        List<Siparisler> GetGarsonSiparisler(int garsonID);
        List<Siparisler> GetGarsonAktifSiparisler(int garsonID);
    }
}
