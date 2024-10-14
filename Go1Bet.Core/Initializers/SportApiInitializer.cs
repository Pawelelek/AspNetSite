using Go1Bet.Core.Context;
using Go1Bet.Core.Entities.Bonuses;
using Go1Bet.Core.Entities.Sport;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Initializers
{
    public static class SportApiInitializer
    {
        public static async Task SeedSportApi(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateAsyncScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();


                if (!context.SportEvents.Any())
                {
                    #region Event1
                    var event1 = new SportEventEntity() { Name = "УКРАЇНА | ПРЕМЄР ЛІГА", DateStart = DateTime.UtcNow.AddDays(2), DateEnd = DateTime.UtcNow.AddDays(30), Status = "Waiting" };
                    await context.SportEvents.AddAsync(event1);
                    await context.SaveChangesAsync();
                    #region Match1
                    var match1 = new SportMatchEntity() { Name = "Match1", SportEventId = event1.Id, DateStart = DateTime.UtcNow.AddDays(2), DateEnd = DateTime.UtcNow.AddDays(3) };
                    await context.SportMatches.AddAsync(match1);
                    await context.SaveChangesAsync();

                    var opponent1 = new OpponentEntity() { Name = "Верес", CountryCode = "UA", SportMatchId = match1.Id };
                    await context.Opponents.AddAsync(opponent1);
                    await context.SaveChangesAsync();

                    //var persons1 = new List<PersonEntity>() 
                    //{
                    //    new PersonEntity { Name = "Головаченко, Микола", Number = "#47", Position = "ГК", OpponentId = opponent1.Id },
                    //    new PersonEntity { Name = "Карасевич, Тарас", Number = "#23", Position = "ГК", OpponentId = opponent1.Id },
                    //    new PersonEntity { Name = "Приходько, Степан", Number = "#1", Position = "ГК", OpponentId = opponent1.Id },
                    //    new PersonEntity { Name = "Малинович, Сергій", Number = "#95", Position = "ЗАХ", OpponentId = opponent1.Id },
                    //    new PersonEntity { Name = "Голуб, Андрій", Number = "#33", Position = "ЗАХ", OpponentId = opponent1.Id },
                    //    new PersonEntity { Name = "Полюхович, Роман", Number = "#3", Position = "ЗАХ", OpponentId = opponent1.Id },
                    //    new PersonEntity { Name = "Кротевич, Олександр", Number = "#67", Position = "ЗАХ", OpponentId = opponent1.Id },
                    //    new PersonEntity { Name = "Овчаренко, Денис", Number = "#57", Position = "ЗАХ", OpponentId = opponent1.Id },
                    //    new PersonEntity { Name = "Кухотко, Олександр", Number = "#71", Position = "ЗАХ", OpponentId = opponent1.Id },
                    //    new PersonEntity { Name = "Стадник, Станіслав", Number = "#17", Position = "ЗАХ", OpponentId = opponent1.Id },
                    //    new PersonEntity { Name = "Маринич, Дмитро", Number = "#29", Position = "ПЗХ", OpponentId = opponent1.Id },
                    //    new PersonEntity { Name = "Шаповал, Ігор", Number = "#10", Position = "ПЗХ", OpponentId = opponent1.Id },
                    //    new PersonEntity { Name = "Станько, Ігор", Number = "#6", Position = "ПЗХ", OpponentId = opponent1.Id },
                    //    new PersonEntity { Name = "Тарасюк, Вадим", Number = "#87", Position = "ПЗХ", OpponentId = opponent1.Id }
                    //};
                    //await context.Persons.AddRangeAsync(persons1);
                    //await context.SaveChangesAsync();

                    var opponent2 = new OpponentEntity() { Name = "Динамо Київ", CountryCode = "UA", SportMatchId = match1.Id };
                    await context.Opponents.AddAsync(opponent2);
                    await context.SaveChangesAsync();

                    //var persons2 = new List<PersonEntity>()
                    //{
                    //    new PersonEntity { Name = "Головаченко, Микола", Number = "#47", Position = "ГК", OpponentId = opponent2.Id },
                    //    new PersonEntity { Name = "Карасевич, Тарас", Number = "#23", Position = "ГК", OpponentId = opponent2.Id },
                    //    new PersonEntity { Name = "Приходько, Степан", Number = "#1", Position = "ГК", OpponentId = opponent2.Id },
                    //    new PersonEntity { Name = "Малинович, Сергій", Number = "#95", Position = "ЗАХ", OpponentId = opponent2.Id },
                    //    new PersonEntity { Name = "Голуб, Андрій", Number = "#33", Position = "ЗАХ", OpponentId = opponent2.Id },
                    //    new PersonEntity { Name = "Полюхович, Роман", Number = "#3", Position = "ЗАХ", OpponentId = opponent2.Id },
                    //    new PersonEntity { Name = "Кротевич, Олександр", Number = "#67", Position = "ЗАХ", OpponentId = opponent2.Id },
                    //    new PersonEntity { Name = "Овчаренко, Денис", Number = "#57", Position = "ЗАХ", OpponentId = opponent2.Id },
                    //    new PersonEntity { Name = "Кухотко, Олександр", Number = "#71", Position = "ЗАХ", OpponentId = opponent2.Id },
                    //    new PersonEntity { Name = "Стадник, Станіслав", Number = "#17", Position = "ЗАХ", OpponentId = opponent2.Id },
                    //    new PersonEntity { Name = "Маринич, Дмитро", Number = "#29", Position = "ПЗХ", OpponentId = opponent2.Id },
                    //    new PersonEntity { Name = "Шаповал, Ігор", Number = "#10", Position = "ПЗХ", OpponentId = opponent2.Id },
                    //    new PersonEntity { Name = "Станько, Ігор", Number = "#6", Position = "ПЗХ", OpponentId = opponent2.Id },
                    //    new PersonEntity { Name = "Тарасюк, Вадим", Number = "#87", Position = "ПЗХ", OpponentId = opponent2.Id }
                    //};
                    //await context.Persons.AddRangeAsync(persons2);
                    //await context.SaveChangesAsync();

                    var odds1 = new List<OddEntity>()
                    {
                        new OddEntity { Name = $"{opponent1.Name} - WIN", Value = (decimal)2.1, OpponentId = opponent1.Id, SportMatchId = match1.Id, Type = "Win1" },
                        new OddEntity { Name = $"{opponent1.Name} {opponent2.Name} - DRAW", Value = (decimal)2.5, SportMatchId = match1.Id, Type = "Draw" },
                        new OddEntity { Name = $"{opponent2.Name} - WIN", Value = (decimal)2.3, OpponentId = opponent2.Id, SportMatchId = match1.Id, Type = "Win2" },

                        new OddEntity { Name = $"{opponent1.Name} - GOAL", Value = (decimal)1.75, OpponentId = opponent1.Id, SportMatchId = match1.Id, Type = "Goal1" },
                        new OddEntity { Name = $"{opponent2.Name} - GOAL", Value = (decimal)2.45, OpponentId = opponent2.Id, SportMatchId = match1.Id, Type = "Goal2" },

                        new OddEntity { Name = $"{opponent1.Name} - TOTAL", Value = (decimal)1.8, OpponentId = opponent1.Id, SportMatchId = match1.Id, Type = "Total1" },
                        new OddEntity { Name = $"{opponent2.Name} - TOTAL", Value = (decimal)1.9, OpponentId = opponent2.Id, SportMatchId = match1.Id, Type = "Total2" },
                    };
                    await context.Odds.AddRangeAsync(odds1);
                    await context.SaveChangesAsync();
                    #endregion

                    #region Match2
                    var match2 = new SportMatchEntity() { Name = "Match2", SportEventId = event1.Id, DateStart = DateTime.UtcNow.AddDays(2), DateEnd = DateTime.UtcNow.AddDays(3) };
                    await context.SportMatches.AddAsync(match2);
                    await context.SaveChangesAsync();

                    var opponent3 = new OpponentEntity() { Name = "ФК Полісся", CountryCode = "UA", SportMatchId = match2.Id };
                    await context.Opponents.AddAsync(opponent3);
                    await context.SaveChangesAsync();

                    //var persons3 = new List<PersonEntity>()
                    //{
                    //    new PersonEntity { Name = "Головаченко, Микола", Number = "#47", Position = "ГК", OpponentId = opponent3.Id },
                    //    new PersonEntity { Name = "Карасевич, Тарас", Number = "#23", Position = "ГК", OpponentId = opponent3.Id },
                    //    new PersonEntity { Name = "Приходько, Степан", Number = "#1", Position = "ГК", OpponentId = opponent3.Id },
                    //    new PersonEntity { Name = "Малинович, Сергій", Number = "#95", Position = "ЗАХ", OpponentId = opponent3.Id },
                    //    new PersonEntity { Name = "Голуб, Андрій", Number = "#33", Position = "ЗАХ", OpponentId = opponent3.Id },
                    //    new PersonEntity { Name = "Полюхович, Роман", Number = "#3", Position = "ЗАХ", OpponentId = opponent3.Id },
                    //    new PersonEntity { Name = "Кротевич, Олександр", Number = "#67", Position = "ЗАХ", OpponentId = opponent3.Id },
                    //    new PersonEntity { Name = "Овчаренко, Денис", Number = "#57", Position = "ЗАХ", OpponentId = opponent3.Id },
                    //    new PersonEntity { Name = "Кухотко, Олександр", Number = "#71", Position = "ЗАХ", OpponentId = opponent3.Id },
                    //    new PersonEntity { Name = "Стадник, Станіслав", Number = "#17", Position = "ЗАХ", OpponentId = opponent3.Id },
                    //    new PersonEntity { Name = "Маринич, Дмитро", Number = "#29", Position = "ПЗХ", OpponentId = opponent3.Id },
                    //    new PersonEntity { Name = "Шаповал, Ігор", Number = "#10", Position = "ПЗХ", OpponentId = opponent3.Id },
                    //    new PersonEntity { Name = "Станько, Ігор", Number = "#6", Position = "ПЗХ", OpponentId = opponent3.Id },
                    //    new PersonEntity { Name = "Тарасюк, Вадим", Number = "#87", Position = "ПЗХ", OpponentId = opponent3.Id }
                    //};
                    //await context.Persons.AddRangeAsync(persons3);
                    //await context.SaveChangesAsync();

                    var opponent4 = new OpponentEntity() { Name = "Оболонь", CountryCode = "UA", SportMatchId = match2.Id };
                    await context.Opponents.AddAsync(opponent4);
                    await context.SaveChangesAsync();

                    //var persons4 = new List<PersonEntity>()
                    //{
                    //    new PersonEntity { Name = "Головаченко, Микола", Number = "#47", Position = "ГК", OpponentId = opponent4.Id },
                    //    new PersonEntity { Name = "Карасевич, Тарас", Number = "#23", Position = "ГК", OpponentId = opponent4.Id },
                    //    new PersonEntity { Name = "Приходько, Степан", Number = "#1", Position = "ГК", OpponentId = opponent4.Id },
                    //    new PersonEntity { Name = "Малинович, Сергій", Number = "#95", Position = "ЗАХ", OpponentId = opponent4.Id },
                    //    new PersonEntity { Name = "Голуб, Андрій", Number = "#33", Position = "ЗАХ", OpponentId = opponent4.Id },
                    //    new PersonEntity { Name = "Полюхович, Роман", Number = "#3", Position = "ЗАХ", OpponentId = opponent4.Id },
                    //    new PersonEntity { Name = "Кротевич, Олександр", Number = "#67", Position = "ЗАХ", OpponentId = opponent4.Id },
                    //    new PersonEntity { Name = "Овчаренко, Денис", Number = "#57", Position = "ЗАХ", OpponentId = opponent4.Id },
                    //    new PersonEntity { Name = "Кухотко, Олександр", Number = "#71", Position = "ЗАХ", OpponentId = opponent4.Id },
                    //    new PersonEntity { Name = "Стадник, Станіслав", Number = "#17", Position = "ЗАХ", OpponentId = opponent4.Id },
                    //    new PersonEntity { Name = "Маринич, Дмитро", Number = "#29", Position = "ПЗХ", OpponentId = opponent4.Id },
                    //    new PersonEntity { Name = "Шаповал, Ігор", Number = "#10", Position = "ПЗХ", OpponentId = opponent4.Id },
                    //    new PersonEntity { Name = "Станько, Ігор", Number = "#6", Position = "ПЗХ", OpponentId = opponent4.Id },
                    //    new PersonEntity { Name = "Тарасюк, Вадим", Number = "#87", Position = "ПЗХ", OpponentId = opponent4.Id }
                    //};
                    //await context.Persons.AddRangeAsync(persons4);
                    //await context.SaveChangesAsync();
                    var odds2 = new List<OddEntity>()
                    {
                        new OddEntity { Name = $"{opponent3.Name} - WIN", Value = (decimal)2.1, OpponentId = opponent3.Id, SportMatchId = match2.Id, Type = "Win1" },
                        new OddEntity { Name = $"{opponent3.Name} {opponent4.Name} - DRAW", Value = (decimal)2.5, SportMatchId = match2.Id, Type = "Draw" },
                        new OddEntity { Name = $"{opponent4.Name} - WIN", Value = (decimal)2.3, OpponentId = opponent4.Id, SportMatchId = match2.Id, Type = "Win2" },

                        new OddEntity { Name = $"{opponent3.Name} - GOAL", Value = (decimal)1.75, OpponentId = opponent3.Id, SportMatchId = match2.Id, Type = "Goal1" },
                        new OddEntity { Name = $"{opponent4.Name} - GOAL", Value = (decimal)2.45, OpponentId = opponent4.Id, SportMatchId = match2.Id, Type = "Goal2" },

                        new OddEntity { Name = $"{opponent3.Name} - TOTAL", Value = (decimal)1.8, OpponentId = opponent3.Id, SportMatchId = match2.Id, Type = "Total1" },
                        new OddEntity { Name = $"{opponent4.Name} - TOTAL", Value = (decimal)1.9, OpponentId = opponent4.Id, SportMatchId = match2.Id, Type = "Total2" },
                    };
                    await context.Odds.AddRangeAsync(odds2);
                    await context.SaveChangesAsync();
                    #endregion

                    #region Match3
                    var match3 = new SportMatchEntity() { Name = "Match3", SportEventId = event1.Id, DateStart = DateTime.UtcNow.AddDays(2), DateEnd = DateTime.UtcNow.AddDays(3) };
                    await context.SportMatches.AddAsync(match3);
                    await context.SaveChangesAsync();

                    var opponent5 = new OpponentEntity() { Name = "Рух Львів", CountryCode = "UA", SportMatchId = match3.Id };
                    await context.Opponents.AddAsync(opponent5);
                    await context.SaveChangesAsync();

                    //var persons5 = new List<PersonEntity>()
                    //{
                    //    new PersonEntity { Name = "Головаченко, Микола", Number = "#47", Position = "ГК", OpponentId = opponent5.Id },
                    //    new PersonEntity { Name = "Карасевич, Тарас", Number = "#23", Position = "ГК", OpponentId = opponent5.Id },
                    //    new PersonEntity { Name = "Приходько, Степан", Number = "#1", Position = "ГК", OpponentId = opponent5.Id },
                    //    new PersonEntity { Name = "Малинович, Сергій", Number = "#95", Position = "ЗАХ", OpponentId = opponent5.Id },
                    //    new PersonEntity { Name = "Голуб, Андрій", Number = "#33", Position = "ЗАХ", OpponentId = opponent5.Id },
                    //    new PersonEntity { Name = "Полюхович, Роман", Number = "#3", Position = "ЗАХ", OpponentId = opponent5.Id },
                    //    new PersonEntity { Name = "Кротевич, Олександр", Number = "#67", Position = "ЗАХ", OpponentId = opponent5.Id },
                    //    new PersonEntity { Name = "Овчаренко, Денис", Number = "#57", Position = "ЗАХ", OpponentId = opponent5.Id },
                    //    new PersonEntity { Name = "Кухотко, Олександр", Number = "#71", Position = "ЗАХ", OpponentId = opponent5.Id },
                    //    new PersonEntity { Name = "Стадник, Станіслав", Number = "#17", Position = "ЗАХ", OpponentId = opponent5.Id },
                    //    new PersonEntity { Name = "Маринич, Дмитро", Number = "#29", Position = "ПЗХ", OpponentId = opponent5.Id },
                    //    new PersonEntity { Name = "Шаповал, Ігор", Number = "#10", Position = "ПЗХ", OpponentId = opponent5.Id },
                    //    new PersonEntity { Name = "Станько, Ігор", Number = "#6", Position = "ПЗХ", OpponentId = opponent5.Id },
                    //    new PersonEntity { Name = "Тарасюк, Вадим", Number = "#87", Position = "ПЗХ", OpponentId = opponent5.Id }
                    //};
                    //await context.Persons.AddRangeAsync(persons5);
                    //await context.SaveChangesAsync();

                    var opponent6 = new OpponentEntity() { Name = "Зоря Луганськ", CountryCode = "UA", SportMatchId = match3.Id };
                    await context.Opponents.AddAsync(opponent6);
                    await context.SaveChangesAsync();

                    //var persons6 = new List<PersonEntity>()
                    //{
                    //    new PersonEntity { Name = "Головаченко, Микола", Number = "#47", Position = "ГК", OpponentId = opponent6.Id },
                    //    new PersonEntity { Name = "Карасевич, Тарас", Number = "#23", Position = "ГК", OpponentId = opponent6.Id },
                    //    new PersonEntity { Name = "Приходько, Степан", Number = "#1", Position = "ГК", OpponentId = opponent6.Id },
                    //    new PersonEntity { Name = "Малинович, Сергій", Number = "#95", Position = "ЗАХ", OpponentId = opponent6.Id },
                    //    new PersonEntity { Name = "Голуб, Андрій", Number = "#33", Position = "ЗАХ", OpponentId = opponent6.Id },
                    //    new PersonEntity { Name = "Полюхович, Роман", Number = "#3", Position = "ЗАХ", OpponentId = opponent6.Id },
                    //    new PersonEntity { Name = "Кротевич, Олександр", Number = "#67", Position = "ЗАХ", OpponentId = opponent6.Id },
                    //    new PersonEntity { Name = "Овчаренко, Денис", Number = "#57", Position = "ЗАХ", OpponentId = opponent6.Id },
                    //    new PersonEntity { Name = "Кухотко, Олександр", Number = "#71", Position = "ЗАХ", OpponentId = opponent6.Id },
                    //    new PersonEntity { Name = "Стадник, Станіслав", Number = "#17", Position = "ЗАХ", OpponentId = opponent6.Id },
                    //    new PersonEntity { Name = "Маринич, Дмитро", Number = "#29", Position = "ПЗХ", OpponentId = opponent6.Id },
                    //    new PersonEntity { Name = "Шаповал, Ігор", Number = "#10", Position = "ПЗХ", OpponentId = opponent6.Id },
                    //    new PersonEntity { Name = "Станько, Ігор", Number = "#6", Position = "ПЗХ", OpponentId = opponent6.Id },
                    //    new PersonEntity { Name = "Тарасюк, Вадим", Number = "#87", Position = "ПЗХ", OpponentId = opponent6.Id }
                    //};
                    //await context.Persons.AddRangeAsync(persons6);
                    //await context.SaveChangesAsync();
                    var odds3 = new List<OddEntity>()
                    {
                        new OddEntity { Name = $"{opponent5.Name} - WIN", Value = (decimal)2.1, OpponentId = opponent5.Id, SportMatchId = match3.Id, Type = "Win1" },
                        new OddEntity { Name = $"{opponent5.Name} {opponent6.Name} - DRAW", Value = (decimal)2.5, SportMatchId = match3.Id, Type = "Draw" },
                        new OddEntity { Name = $"{opponent6.Name} - WIN", Value = (decimal)2.3, OpponentId = opponent6.Id, SportMatchId = match3.Id, Type = "Win2" },

                        new OddEntity { Name = $"{opponent5.Name} - GOAL", Value = (decimal)1.75, OpponentId = opponent5.Id, SportMatchId = match3.Id, Type = "Goal1" },
                        new OddEntity { Name = $"{opponent6.Name} - GOAL", Value = (decimal)2.45, OpponentId = opponent6.Id, SportMatchId = match3.Id, Type = "Goal2" },

                        new OddEntity { Name = $"{opponent5.Name} - TOTAL", Value = (decimal)1.8, OpponentId = opponent5.Id, SportMatchId = match3.Id, Type = "Total1" },
                        new OddEntity { Name = $"{opponent6.Name} - TOTAL", Value = (decimal)1.9, OpponentId = opponent6.Id, SportMatchId = match3.Id, Type = "Total2" },
                    };
                    await context.Odds.AddRangeAsync(odds3);
                    await context.SaveChangesAsync();
                    #endregion

                    #region Match4
                    var match4 = new SportMatchEntity() { Name = "Match4", SportEventId = event1.Id, DateStart = DateTime.UtcNow.AddDays(2), DateEnd = DateTime.UtcNow.AddDays(3) };
                    await context.SportMatches.AddAsync(match4);
                    await context.SaveChangesAsync();

                    var opponent7 = new OpponentEntity() { Name = "Кривбас", CountryCode = "UA", SportMatchId = match4.Id };
                    await context.Opponents.AddAsync(opponent7);
                    await context.SaveChangesAsync();

                    //var persons7 = new List<PersonEntity>()
                    //{
                    //    new PersonEntity { Name = "Головаченко, Микола", Number = "#47", Position = "ГК", OpponentId = opponent7.Id },
                    //    new PersonEntity { Name = "Карасевич, Тарас", Number = "#23", Position = "ГК", OpponentId = opponent7.Id },
                    //    new PersonEntity { Name = "Приходько, Степан", Number = "#1", Position = "ГК", OpponentId = opponent7.Id },
                    //    new PersonEntity { Name = "Малинович, Сергій", Number = "#95", Position = "ЗАХ", OpponentId = opponent7.Id },
                    //    new PersonEntity { Name = "Голуб, Андрій", Number = "#33", Position = "ЗАХ", OpponentId = opponent7.Id },
                    //    new PersonEntity { Name = "Полюхович, Роман", Number = "#3", Position = "ЗАХ", OpponentId = opponent7.Id },
                    //    new PersonEntity { Name = "Кротевич, Олександр", Number = "#67", Position = "ЗАХ", OpponentId = opponent7.Id },
                    //    new PersonEntity { Name = "Овчаренко, Денис", Number = "#57", Position = "ЗАХ", OpponentId = opponent7.Id },
                    //    new PersonEntity { Name = "Кухотко, Олександр", Number = "#71", Position = "ЗАХ", OpponentId = opponent7.Id },
                    //    new PersonEntity { Name = "Стадник, Станіслав", Number = "#17", Position = "ЗАХ", OpponentId = opponent7.Id },
                    //    new PersonEntity { Name = "Маринич, Дмитро", Number = "#29", Position = "ПЗХ", OpponentId = opponent7.Id },
                    //    new PersonEntity { Name = "Шаповал, Ігор", Number = "#10", Position = "ПЗХ", OpponentId = opponent7.Id },
                    //    new PersonEntity { Name = "Станько, Ігор", Number = "#6", Position = "ПЗХ", OpponentId = opponent7.Id },
                    //    new PersonEntity { Name = "Тарасюк, Вадим", Number = "#87", Position = "ПЗХ", OpponentId = opponent7.Id }
                    //};
                    //await context.Persons.AddRangeAsync(persons7);
                    //await context.SaveChangesAsync();

                    var opponent8 = new OpponentEntity() { Name = "Інгулець", CountryCode = "UA", SportMatchId = match4.Id };
                    await context.Opponents.AddAsync(opponent8);
                    await context.SaveChangesAsync();

                    //var persons8 = new List<PersonEntity>()
                    //{
                    //    new PersonEntity { Name = "Головаченко, Микола", Number = "#47", Position = "ГК", OpponentId = opponent8.Id },
                    //    new PersonEntity { Name = "Карасевич, Тарас", Number = "#23", Position = "ГК", OpponentId = opponent8.Id },
                    //    new PersonEntity { Name = "Приходько, Степан", Number = "#1", Position = "ГК", OpponentId = opponent8.Id },
                    //    new PersonEntity { Name = "Малинович, Сергій", Number = "#95", Position = "ЗАХ", OpponentId = opponent8.Id },
                    //    new PersonEntity { Name = "Голуб, Андрій", Number = "#33", Position = "ЗАХ", OpponentId = opponent8.Id },
                    //    new PersonEntity { Name = "Полюхович, Роман", Number = "#3", Position = "ЗАХ", OpponentId = opponent8.Id },
                    //    new PersonEntity { Name = "Кротевич, Олександр", Number = "#67", Position = "ЗАХ", OpponentId = opponent8.Id },
                    //    new PersonEntity { Name = "Овчаренко, Денис", Number = "#57", Position = "ЗАХ", OpponentId = opponent8.Id },
                    //    new PersonEntity { Name = "Кухотко, Олександр", Number = "#71", Position = "ЗАХ", OpponentId = opponent8.Id },
                    //    new PersonEntity { Name = "Стадник, Станіслав", Number = "#17", Position = "ЗАХ", OpponentId = opponent8.Id },
                    //    new PersonEntity { Name = "Маринич, Дмитро", Number = "#29", Position = "ПЗХ", OpponentId = opponent8.Id },
                    //    new PersonEntity { Name = "Шаповал, Ігор", Number = "#10", Position = "ПЗХ", OpponentId = opponent8.Id },
                    //    new PersonEntity { Name = "Станько, Ігор", Number = "#6", Position = "ПЗХ", OpponentId = opponent8.Id },
                    //    new PersonEntity { Name = "Тарасюк, Вадим", Number = "#87", Position = "ПЗХ", OpponentId = opponent8.Id }
                    //};
                    //await context.Persons.AddRangeAsync(persons8);
                    //await context.SaveChangesAsync();
                    var odds4 = new List<OddEntity>()
                    {
                        new OddEntity { Name = $"{opponent7.Name} - WIN", Value = (decimal)1.9, OpponentId = opponent7.Id, SportMatchId = match4.Id, Type = "Win1" },
                        new OddEntity { Name = $"{opponent7.Name} {opponent8.Name} - DRAW", Value = (decimal)2.0, SportMatchId = match4.Id, Type = "Draw" },
                        new OddEntity { Name = $"{opponent8.Name} - WIN", Value = (decimal)2.25, OpponentId = opponent8.Id, SportMatchId = match4.Id, Type = "Win2" },

                        new OddEntity { Name = $"{opponent7.Name} - GOAL", Value = (decimal)1.75, OpponentId = opponent7.Id, SportMatchId = match4.Id, Type = "Goal1" },
                        new OddEntity { Name = $"{opponent8.Name} - GOAL", Value = (decimal)2.45, OpponentId = opponent8.Id, SportMatchId = match4.Id, Type = "Goal2" },

                        new OddEntity { Name = $"{opponent7.Name} - TOTAL", Value = (decimal)1.8, OpponentId = opponent7.Id, SportMatchId = match4.Id, Type = "Total1" },
                        new OddEntity { Name = $"{opponent8.Name} - TOTAL", Value = (decimal)1.9, OpponentId = opponent8.Id, SportMatchId = match4.Id, Type = "Total2" },
                    };
                    await context.Odds.AddRangeAsync(odds4);
                    await context.SaveChangesAsync();
                    #endregion

                    #region Match5
                    var match5 = new SportMatchEntity() { Name = "Match5", SportEventId = event1.Id, DateStart = DateTime.UtcNow.AddDays(2), DateEnd = DateTime.UtcNow.AddDays(3) };
                    await context.SportMatches.AddAsync(match5);
                    await context.SaveChangesAsync();

                    var opponent9 = new OpponentEntity() { Name = "ЛНЗ Черкаси", CountryCode = "UA", SportMatchId = match5.Id };
                    await context.Opponents.AddAsync(opponent9);
                    await context.SaveChangesAsync();

                    //var persons9 = new List<PersonEntity>()
                    //{
                    //    new PersonEntity { Name = "Головаченко, Микола", Number = "#47", Position = "ГК", OpponentId = opponent9.Id },
                    //    new PersonEntity { Name = "Карасевич, Тарас", Number = "#23", Position = "ГК", OpponentId = opponent9.Id },
                    //    new PersonEntity { Name = "Приходько, Степан", Number = "#1", Position = "ГК", OpponentId = opponent9.Id },
                    //    new PersonEntity { Name = "Малинович, Сергій", Number = "#95", Position = "ЗАХ", OpponentId = opponent9.Id },
                    //    new PersonEntity { Name = "Голуб, Андрій", Number = "#33", Position = "ЗАХ", OpponentId = opponent9.Id },
                    //    new PersonEntity { Name = "Полюхович, Роман", Number = "#3", Position = "ЗАХ", OpponentId = opponent9.Id },
                    //    new PersonEntity { Name = "Кротевич, Олександр", Number = "#67", Position = "ЗАХ", OpponentId = opponent9.Id },
                    //    new PersonEntity { Name = "Овчаренко, Денис", Number = "#57", Position = "ЗАХ", OpponentId = opponent9.Id },
                    //    new PersonEntity { Name = "Кухотко, Олександр", Number = "#71", Position = "ЗАХ", OpponentId = opponent9.Id },
                    //    new PersonEntity { Name = "Стадник, Станіслав", Number = "#17", Position = "ЗАХ", OpponentId = opponent9.Id },
                    //    new PersonEntity { Name = "Маринич, Дмитро", Number = "#29", Position = "ПЗХ", OpponentId = opponent9.Id },
                    //    new PersonEntity { Name = "Шаповал, Ігор", Number = "#10", Position = "ПЗХ", OpponentId = opponent9.Id },
                    //    new PersonEntity { Name = "Станько, Ігор", Number = "#6", Position = "ПЗХ", OpponentId = opponent9.Id },
                    //    new PersonEntity { Name = "Тарасюк, Вадим", Number = "#87", Position = "ПЗХ", OpponentId = opponent9.Id }
                    //};
                    //await context.Persons.AddRangeAsync(persons9);
                    //await context.SaveChangesAsync();

                    var opponent10 = new OpponentEntity() { Name = "ФК Карпати Львів", CountryCode = "UA", SportMatchId = match5.Id };
                    await context.Opponents.AddAsync(opponent10);
                    await context.SaveChangesAsync();

                    //var persons10 = new List<PersonEntity>()
                    //{
                    //    new PersonEntity { Name = "Головаченко, Микола", Number = "#47", Position = "ГК", OpponentId = opponent10.Id },
                    //    new PersonEntity { Name = "Карасевич, Тарас", Number = "#23", Position = "ГК", OpponentId = opponent10.Id },
                    //    new PersonEntity { Name = "Приходько, Степан", Number = "#1", Position = "ГК", OpponentId = opponent10.Id },
                    //    new PersonEntity { Name = "Малинович, Сергій", Number = "#95", Position = "ЗАХ", OpponentId = opponent10.Id },
                    //    new PersonEntity { Name = "Голуб, Андрій", Number = "#33", Position = "ЗАХ", OpponentId = opponent10.Id },
                    //    new PersonEntity { Name = "Полюхович, Роман", Number = "#3", Position = "ЗАХ", OpponentId = opponent10.Id },
                    //    new PersonEntity { Name = "Кротевич, Олександр", Number = "#67", Position = "ЗАХ", OpponentId = opponent10.Id },
                    //    new PersonEntity { Name = "Овчаренко, Денис", Number = "#57", Position = "ЗАХ", OpponentId = opponent10.Id },
                    //    new PersonEntity { Name = "Кухотко, Олександр", Number = "#71", Position = "ЗАХ", OpponentId = opponent10.Id },
                    //    new PersonEntity { Name = "Стадник, Станіслав", Number = "#17", Position = "ЗАХ", OpponentId = opponent10.Id },
                    //    new PersonEntity { Name = "Маринич, Дмитро", Number = "#29", Position = "ПЗХ", OpponentId = opponent10.Id },
                    //    new PersonEntity { Name = "Шаповал, Ігор", Number = "#10", Position = "ПЗХ", OpponentId = opponent10.Id },
                    //    new PersonEntity { Name = "Станько, Ігор", Number = "#6", Position = "ПЗХ", OpponentId = opponent10.Id },
                    //    new PersonEntity { Name = "Тарасюк, Вадим", Number = "#87", Position = "ПЗХ", OpponentId = opponent10.Id }
                    //};
                    //await context.Persons.AddRangeAsync(persons10);
                    //await context.SaveChangesAsync();
                    var odds5 = new List<OddEntity>()
                    {
                        new OddEntity { Name = $"{opponent9.Name} - WIN", Value = (decimal)1.4, OpponentId = opponent9.Id, SportMatchId = match5.Id, Type = "Win1" },
                        new OddEntity { Name = $"{opponent9.Name} {opponent10.Name} - DRAW", Value = (decimal)2, SportMatchId = match5.Id, Type = "Draw" },
                        new OddEntity { Name = $"{opponent10.Name} - WIN", Value = (decimal)2.1, OpponentId = opponent10.Id, SportMatchId = match5.Id, Type = "Win2" },

                        new OddEntity { Name = $"{opponent9.Name} - GOAL", Value = (decimal)1.75, OpponentId = opponent9.Id, SportMatchId = match5.Id, Type = "Goal1" },
                        new OddEntity { Name = $"{opponent10.Name} - GOAL", Value = (decimal)2.45, OpponentId = opponent10.Id, SportMatchId = match5.Id, Type = "Goal2" },

                        new OddEntity { Name = $"{opponent9.Name} - TOTAL", Value = (decimal)1.8, OpponentId = opponent9.Id, SportMatchId = match5.Id, Type = "Total1" },
                        new OddEntity { Name = $"{opponent10.Name} - TOTAL", Value = (decimal)1.9, OpponentId = opponent10.Id, SportMatchId = match5.Id, Type = "Total2" },
                    };
                    await context.Odds.AddRangeAsync(odds5);
                    await context.SaveChangesAsync();
                    #endregion

                    #region Match6
                    var match6 = new SportMatchEntity() { Name = "Match5", SportEventId = event1.Id, DateStart = DateTime.UtcNow.AddDays(2), DateEnd = DateTime.UtcNow.AddDays(3) };
                    await context.SportMatches.AddAsync(match6);
                    await context.SaveChangesAsync();

                    var opponent11 = new OpponentEntity() { Name = "Лівий Берег", CountryCode = "UA", SportMatchId = match6.Id };
                    await context.Opponents.AddAsync(opponent11);
                    await context.SaveChangesAsync();

                    //var persons11 = new List<PersonEntity>()
                    //{
                    //    new PersonEntity { Name = "Головаченко, Микола", Number = "#47", Position = "ГК", OpponentId = opponent11.Id },
                    //    new PersonEntity { Name = "Карасевич, Тарас", Number = "#23", Position = "ГК", OpponentId = opponent11.Id },
                    //    new PersonEntity { Name = "Приходько, Степан", Number = "#1", Position = "ГК", OpponentId = opponent11.Id },
                    //    new PersonEntity { Name = "Малинович, Сергій", Number = "#95", Position = "ЗАХ", OpponentId = opponent11.Id },
                    //    new PersonEntity { Name = "Голуб, Андрій", Number = "#33", Position = "ЗАХ", OpponentId = opponent11.Id },
                    //    new PersonEntity { Name = "Полюхович, Роман", Number = "#3", Position = "ЗАХ", OpponentId = opponent11.Id },
                    //    new PersonEntity { Name = "Кротевич, Олександр", Number = "#67", Position = "ЗАХ", OpponentId = opponent11.Id },
                    //    new PersonEntity { Name = "Овчаренко, Денис", Number = "#57", Position = "ЗАХ", OpponentId = opponent11.Id },
                    //    new PersonEntity { Name = "Кухотко, Олександр", Number = "#71", Position = "ЗАХ", OpponentId = opponent11.Id },
                    //    new PersonEntity { Name = "Стадник, Станіслав", Number = "#17", Position = "ЗАХ", OpponentId = opponent11.Id },
                    //    new PersonEntity { Name = "Маринич, Дмитро", Number = "#29", Position = "ПЗХ", OpponentId = opponent11.Id },
                    //    new PersonEntity { Name = "Шаповал, Ігор", Number = "#10", Position = "ПЗХ", OpponentId = opponent11.Id },
                    //    new PersonEntity { Name = "Станько, Ігор", Number = "#6", Position = "ПЗХ", OpponentId = opponent11.Id },
                    //    new PersonEntity { Name = "Тарасюк, Вадим", Number = "#87", Position = "ПЗХ", OpponentId = opponent11.Id }
                    //};
                    //await context.Persons.AddRangeAsync(persons11);
                    //await context.SaveChangesAsync();

                    var opponent12 = new OpponentEntity() { Name = "Чорноморець Одеса", CountryCode = "UA", SportMatchId = match6.Id };
                    await context.Opponents.AddAsync(opponent12);
                    await context.SaveChangesAsync();

                    //var persons12 = new List<PersonEntity>()
                    //{
                    //    new PersonEntity { Name = "Головаченко, Микола", Number = "#47", Position = "ГК", OpponentId = opponent12.Id },
                    //    new PersonEntity { Name = "Карасевич, Тарас", Number = "#23", Position = "ГК", OpponentId = opponent12.Id },
                    //    new PersonEntity { Name = "Приходько, Степан", Number = "#1", Position = "ГК", OpponentId = opponent12.Id },
                    //    new PersonEntity { Name = "Малинович, Сергій", Number = "#95", Position = "ЗАХ", OpponentId = opponent12.Id },
                    //    new PersonEntity { Name = "Голуб, Андрій", Number = "#33", Position = "ЗАХ", OpponentId = opponent12.Id },
                    //    new PersonEntity { Name = "Полюхович, Роман", Number = "#3", Position = "ЗАХ", OpponentId = opponent12.Id },
                    //    new PersonEntity { Name = "Кротевич, Олександр", Number = "#67", Position = "ЗАХ", OpponentId = opponent12.Id },
                    //    new PersonEntity { Name = "Овчаренко, Денис", Number = "#57", Position = "ЗАХ", OpponentId = opponent12.Id },
                    //    new PersonEntity { Name = "Кухотко, Олександр", Number = "#71", Position = "ЗАХ", OpponentId = opponent12.Id },
                    //    new PersonEntity { Name = "Стадник, Станіслав", Number = "#17", Position = "ЗАХ", OpponentId = opponent12.Id },
                    //    new PersonEntity { Name = "Маринич, Дмитро", Number = "#29", Position = "ПЗХ", OpponentId = opponent12.Id },
                    //    new PersonEntity { Name = "Шаповал, Ігор", Number = "#10", Position = "ПЗХ", OpponentId = opponent12.Id },
                    //    new PersonEntity { Name = "Станько, Ігор", Number = "#6", Position = "ПЗХ", OpponentId = opponent12.Id },
                    //    new PersonEntity { Name = "Тарасюк, Вадим", Number = "#87", Position = "ПЗХ", OpponentId = opponent12.Id }
                    //};
                    //await context.Persons.AddRangeAsync(persons12);
                    //await context.SaveChangesAsync();
                    var odds6 = new List<OddEntity>()
                    {
                        new OddEntity { Name = $"{opponent11.Name} - WIN", Value = (decimal)2.5, OpponentId = opponent11.Id, SportMatchId = match6.Id, Type = "Win1" },
                        new OddEntity { Name = $"{opponent11.Name} {opponent12.Name} - DRAW", Value = (decimal)1.9, SportMatchId = match6.Id, Type = "Draw" },
                        new OddEntity { Name = $"{opponent12.Name} - WIN", Value = (decimal)2.3, OpponentId = opponent12.Id, SportMatchId = match6.Id, Type = "Win2" },

                        new OddEntity { Name = $"{opponent11.Name} - GOAL", Value = (decimal)1.75, OpponentId = opponent11.Id, SportMatchId = match6.Id, Type = "Goal1" },
                        new OddEntity { Name = $"{opponent12.Name} - GOAL", Value = (decimal)1.9, OpponentId = opponent12.Id, SportMatchId = match6.Id, Type = "Goal2" },

                        new OddEntity { Name = $"{opponent11.Name} - TOTAL", Value = (decimal)2.3, OpponentId = opponent11.Id, SportMatchId = match6.Id, Type = "Total1" },
                        new OddEntity { Name = $"{opponent12.Name} - TOTAL", Value = (decimal)1.8, OpponentId = opponent12.Id, SportMatchId = match6.Id, Type = "Total2" },
                    };
                    await context.Odds.AddRangeAsync(odds6);
                    await context.SaveChangesAsync();
                    #endregion

                    #endregion

                    #region Event2


                    var event2 = new SportEventEntity() { Name = "ЄВРОПА | ЛІГА ЧЕМПІОНІВ. КВАЛІФІКАЦІЯ", DateStart = DateTime.UtcNow.AddDays(2), DateEnd = DateTime.UtcNow.AddDays(30), Status = "Waiting" };
                    await context.SportEvents.AddAsync(event2);
                    await context.SaveChangesAsync();
                    #region Match1
                    var match7 = new SportMatchEntity() { Name = "Match1", SportEventId = event2.Id, DateStart = DateTime.UtcNow.AddDays(2), DateEnd = DateTime.UtcNow.AddDays(3) };
                    await context.SportMatches.AddAsync(match7);
                    await context.SaveChangesAsync();

                    var opponent13 = new OpponentEntity() { Name = "Німечинна", CountryCode = "DE", SportMatchId = match7.Id };
                    await context.Opponents.AddAsync(opponent13);
                    await context.SaveChangesAsync();

                    var opponent14 = new OpponentEntity() { Name = "Данія", CountryCode = "DK", SportMatchId = match7.Id };
                    await context.Opponents.AddAsync(opponent14);
                    await context.SaveChangesAsync();;

                    var odds7 = new List<OddEntity>()
                    {
                        new OddEntity { Name = $"{opponent13.Name} - WIN", Value = (decimal)1.67, OpponentId = opponent13.Id, SportMatchId = match7.Id, Type = "Win1" },
                        new OddEntity { Name = $"{opponent13.Name} {opponent14.Name} - DRAW", Value = (decimal)3.92, SportMatchId = match7.Id, Type = "Draw" },
                        new OddEntity { Name = $"{opponent14.Name} - WIN", Value = (decimal)6.42, OpponentId = opponent14.Id, SportMatchId = match7.Id, Type = "Win2" },

                        new OddEntity { Name = $"{opponent13.Name} - GOAL", Value = (decimal)2.45, OpponentId = opponent13.Id, SportMatchId = match7.Id, Type = "Goal1" },
                        new OddEntity { Name = $"{opponent14.Name} - GOAL", Value = (decimal)1.75, OpponentId = opponent14.Id, SportMatchId = match7.Id, Type = "Goal2" },

                        new OddEntity { Name = $"{opponent13.Name} - TOTAL", Value = (decimal)1.9, OpponentId = opponent13.Id, SportMatchId = match7.Id, Type = "Total1" },
                        new OddEntity { Name = $"{opponent14.Name} - TOTAL", Value = (decimal)1.8, OpponentId = opponent14.Id, SportMatchId = match7.Id, Type = "Total2" },
                    };
                    await context.Odds.AddRangeAsync(odds7);
                    await context.SaveChangesAsync();
                    #endregion

                    #region Match2
                    var match8 = new SportMatchEntity() { Name = "Match1", SportEventId = event2.Id, DateStart = DateTime.UtcNow.AddDays(2), DateEnd = DateTime.UtcNow.AddDays(3) };
                    await context.SportMatches.AddAsync(match8);
                    await context.SaveChangesAsync();

                    var opponent15 = new OpponentEntity() { Name = "Швейцарія", CountryCode = "SW", SportMatchId = match8.Id };
                    await context.Opponents.AddAsync(opponent15);
                    await context.SaveChangesAsync();

                    var opponent16 = new OpponentEntity() { Name = "Італія", CountryCode = "IT", SportMatchId = match8.Id };
                    await context.Opponents.AddAsync(opponent16);
                    await context.SaveChangesAsync(); ;

                    var odds8 = new List<OddEntity>()
                    {
                        new OddEntity { Name = $"{opponent15.Name} - WIN", Value = (decimal)1.59, OpponentId = opponent15.Id, SportMatchId = match8.Id, Type = "Win1" },
                        new OddEntity { Name = $"{opponent15.Name} {opponent16.Name} - DRAW", Value = (decimal)3.23, SportMatchId = match8.Id, Type = "Draw" },
                        new OddEntity { Name = $"{opponent16.Name} - WIN", Value = (decimal)5.30, OpponentId = opponent16.Id, SportMatchId = match8.Id, Type = "Win2" },

                        new OddEntity { Name = $"{opponent15.Name} - GOAL", Value = (decimal)2.1, OpponentId = opponent15.Id, SportMatchId = match8.Id, Type = "Goal1" },
                        new OddEntity { Name = $"{opponent16.Name} - GOAL", Value = (decimal)2.45, OpponentId = opponent16.Id, SportMatchId = match8.Id, Type = "Goal2" },

                        new OddEntity { Name = $"{opponent15.Name} - TOTAL", Value = (decimal)1.8, OpponentId = opponent15.Id, SportMatchId = match8.Id, Type = "Total1" },
                        new OddEntity { Name = $"{opponent16.Name} - TOTAL", Value = (decimal)1.9, OpponentId = opponent16.Id, SportMatchId = match8.Id, Type = "Total2" },
                    };
                    await context.Odds.AddRangeAsync(odds8);
                    await context.SaveChangesAsync();
                    #endregion



                    #endregion

                    #region Event3


                    var event3 = new SportEventEntity() { Name = "США | КУБОК АМЕРИКИ", DateStart = DateTime.UtcNow.AddDays(2), DateEnd = DateTime.UtcNow.AddDays(30), Status = "Waiting" };
                    await context.SportEvents.AddAsync(event3);
                    await context.SaveChangesAsync();
                    #region Match1
                    var match9 = new SportMatchEntity() { Name = "Match1", SportEventId = event3.Id, DateStart = DateTime.UtcNow.AddDays(2), DateEnd = DateTime.UtcNow.AddDays(3) };
                    await context.SportMatches.AddAsync(match9);
                    await context.SaveChangesAsync();

                    var opponent17 = new OpponentEntity() { Name = "Канада", CountryCode = "CA", SportMatchId = match9.Id };
                    await context.Opponents.AddAsync(opponent17);
                    await context.SaveChangesAsync();

                    var opponent18 = new OpponentEntity() { Name = "Чилі", CountryCode = "CHI", SportMatchId = match9.Id };
                    await context.Opponents.AddAsync(opponent18);
                    await context.SaveChangesAsync(); ;

                    var odds9 = new List<OddEntity>()
                    {
                        new OddEntity { Name = $"{opponent17.Name} - WIN", Value = (decimal)2.23, OpponentId = opponent17.Id, SportMatchId = match9.Id, Type = "Win1" },
                        new OddEntity { Name = $"{opponent17.Name} {opponent18.Name} - DRAW", Value = (decimal)5.12, SportMatchId = match9.Id, Type = "Draw" },
                        new OddEntity { Name = $"{opponent18.Name} - WIN", Value = (decimal)7.68, OpponentId = opponent18.Id, SportMatchId = match9.Id, Type = "Win2" },

                        new OddEntity { Name = $"{opponent17.Name} - GOAL", Value = (decimal)1.75, OpponentId = opponent17.Id, SportMatchId = match9.Id, Type = "Goal1" },
                        new OddEntity { Name = $"{opponent18.Name} - GOAL", Value = (decimal)2.45, OpponentId = opponent18.Id, SportMatchId = match9.Id, Type = "Goal2" },

                        new OddEntity { Name = $"{opponent17.Name} - TOTAL", Value = (decimal)1.8, OpponentId = opponent17.Id, SportMatchId = match9.Id, Type = "Total1" },
                        new OddEntity { Name = $"{opponent18.Name} - TOTAL", Value = (decimal)1.9, OpponentId = opponent18.Id, SportMatchId = match9.Id, Type = "Total2" },
                    };
                    await context.Odds.AddRangeAsync(odds9);
                    await context.SaveChangesAsync();
                    #endregion


                    #endregion







                    //await context.SaveChangesAsync();
                }              
            }
        }
    }
}
