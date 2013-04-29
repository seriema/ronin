namespace Ronin.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Ronin.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Ronin.Models.APIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Ronin.Models.APIContext context)
        {
            context.Games.AddOrUpdate(p => p.Name,
               new Game
               {
                   Name = "Q-Bert",
                   Description = "Super awesome",
                   Developer = "Some guy",
                   Publisher = "Someone"
               },
                new Game
                {
                    Name = "Mirror's Edge",
                    Description = "Running and jumping, on rooftops",
                    Developer = "DICE",
                    Publisher = "EA"
                },
                new Game
                {
                    Name = "Mass Effect",
                    Description = "Epic Sci-fi RPG",
                    Developer = "Bioware",
                    Publisher = "EA"
                },
                new Game
                {
                    Name = "Street Fighter",
                    Description = "Fighting game of the century",
                    Developer = "Capcom",
                    Publisher = "Capcom"
                },
                new Game
                {
                    Name = "Snake",
                    Description = "Eat blobs, gain size",
                    Developer = "Some dude",
                    Publisher = "Someone else"
                }
            );
        }
    }
}
