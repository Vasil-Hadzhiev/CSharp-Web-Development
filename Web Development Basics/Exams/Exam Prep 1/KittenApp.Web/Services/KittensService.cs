namespace KittenApp.Web.Services
{
    using Contracts;
    using Data;
    using KittenApp.Models;
    using Models;
    using SimpleMvc.Common;
    using System.Collections.Generic;
    using System.Linq;

    public class KittensService : IKittensService
    {
        public bool Add(string name, int age, string breed)
        {
            using (var db = new KittenAppContext())
            {
                var validBreed = db
                    .Breeds
                    .FirstOrDefault(b => b.Name == breed);

                if (validBreed == null)
                {
                    return false;
                }

                var kitten = new Kitten
                {
                    Name = name,
                    Age = age,
                    Breed = validBreed
                };

                db.Kittens.Add(kitten);
                db.SaveChanges();

                return true;
            }
        }

        public List<string> All()
        {
            using (var db = new KittenAppContext())
            {
                var all = db
                    .Kittens
                    .Select(k => new KittenViewModel
                    {
                        Name = k.Name,
                        Age = k.Age,
                        Breed = k.Breed.Name,

                    });

                var kittens = all
                        .Select(vm =>
                            $@"<div class=""col-4"">
                            <img class=""img-thumbnail"" src=""{BreedHelper.GetImgSource(vm.Breed)}"" alt=""{vm.Name}'s photo"" />
                            <div>
                                <h5>Name: {vm.Name}</h5>
                                <h5>Age: {vm.Age}</h5>
                                <h5>Breed: {vm.Breed}</h5>
                            </div>
                        </div>")
                        .ToList();

                return kittens;
            }
        }
    }
}