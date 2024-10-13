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

                    var persons1 = new List<PersonEntity>() 
                    {
                        new PersonEntity { Name = "Головаченко, Микола", Number = "#47", Position = "ГК", OpponentId = opponent1.Id },
                        new PersonEntity { Name = "Карасевич, Тарас", Number = "#23", Position = "ГК", OpponentId = opponent1.Id },
                        new PersonEntity { Name = "Приходько, Степан", Number = "#1", Position = "ГК", OpponentId = opponent1.Id },
                        new PersonEntity { Name = "Малинович, Сергій", Number = "#95", Position = "ЗАХ", OpponentId = opponent1.Id },
                        new PersonEntity { Name = "Голуб, Андрій", Number = "#33", Position = "ЗАХ", OpponentId = opponent1.Id },
                        new PersonEntity { Name = "Полюхович, Роман", Number = "#3", Position = "ЗАХ", OpponentId = opponent1.Id },
                        new PersonEntity { Name = "Кротевич, Олександр", Number = "#67", Position = "ЗАХ", OpponentId = opponent1.Id },
                        new PersonEntity { Name = "Овчаренко, Денис", Number = "#57", Position = "ЗАХ", OpponentId = opponent1.Id },
                        new PersonEntity { Name = "Кухотко, Олександр", Number = "#71", Position = "ЗАХ", OpponentId = opponent1.Id },
                        new PersonEntity { Name = "Стадник, Станіслав", Number = "#17", Position = "ЗАХ", OpponentId = opponent1.Id },
                        new PersonEntity { Name = "Маринич, Дмитро", Number = "#29", Position = "ПЗХ", OpponentId = opponent1.Id },
                        new PersonEntity { Name = "Шаповал, Ігор", Number = "#10", Position = "ПЗХ", OpponentId = opponent1.Id },
                        new PersonEntity { Name = "Станько, Ігор", Number = "#6", Position = "ПЗХ", OpponentId = opponent1.Id },
                        new PersonEntity { Name = "Тарасюк, Вадим", Number = "#87", Position = "ПЗХ", OpponentId = opponent1.Id }
                    };
                    await context.Persons.AddRangeAsync(persons1);
                    await context.SaveChangesAsync();

                    var opponent2 = new OpponentEntity() { Name = "Динамо Київ", CountryCode = "UA", SportMatchId = match1.Id };
                    await context.Opponents.AddAsync(opponent2);
                    await context.SaveChangesAsync();

                    var persons2 = new List<PersonEntity>()
                    {
                        new PersonEntity { Name = "Головаченко, Микола", Number = "#47", Position = "ГК", OpponentId = opponent2.Id },
                        new PersonEntity { Name = "Карасевич, Тарас", Number = "#23", Position = "ГК", OpponentId = opponent2.Id },
                        new PersonEntity { Name = "Приходько, Степан", Number = "#1", Position = "ГК", OpponentId = opponent2.Id },
                        new PersonEntity { Name = "Малинович, Сергій", Number = "#95", Position = "ЗАХ", OpponentId = opponent2.Id },
                        new PersonEntity { Name = "Голуб, Андрій", Number = "#33", Position = "ЗАХ", OpponentId = opponent2.Id },
                        new PersonEntity { Name = "Полюхович, Роман", Number = "#3", Position = "ЗАХ", OpponentId = opponent2.Id },
                        new PersonEntity { Name = "Кротевич, Олександр", Number = "#67", Position = "ЗАХ", OpponentId = opponent2.Id },
                        new PersonEntity { Name = "Овчаренко, Денис", Number = "#57", Position = "ЗАХ", OpponentId = opponent2.Id },
                        new PersonEntity { Name = "Кухотко, Олександр", Number = "#71", Position = "ЗАХ", OpponentId = opponent2.Id },
                        new PersonEntity { Name = "Стадник, Станіслав", Number = "#17", Position = "ЗАХ", OpponentId = opponent2.Id },
                        new PersonEntity { Name = "Маринич, Дмитро", Number = "#29", Position = "ПЗХ", OpponentId = opponent2.Id },
                        new PersonEntity { Name = "Шаповал, Ігор", Number = "#10", Position = "ПЗХ", OpponentId = opponent2.Id },
                        new PersonEntity { Name = "Станько, Ігор", Number = "#6", Position = "ПЗХ", OpponentId = opponent2.Id },
                        new PersonEntity { Name = "Тарасюк, Вадим", Number = "#87", Position = "ПЗХ", OpponentId = opponent2.Id }
                    };
                    await context.Persons.AddRangeAsync(persons2);
                    await context.SaveChangesAsync();
                    #endregion

                    #region Match2
                    var match2 = new SportMatchEntity() { Name = "Match2", SportEventId = event1.Id, DateStart = DateTime.UtcNow.AddDays(2), DateEnd = DateTime.UtcNow.AddDays(3) };
                    await context.SportMatches.AddAsync(match2);
                    await context.SaveChangesAsync();

                    var opponent3 = new OpponentEntity() { Name = "ФК Полісся", CountryCode = "UA", SportMatchId = match2.Id };
                    await context.Opponents.AddAsync(opponent3);
                    await context.SaveChangesAsync();

                    var persons3 = new List<PersonEntity>()
                    {
                        new PersonEntity { Name = "Головаченко, Микола", Number = "#47", Position = "ГК", OpponentId = opponent3.Id },
                        new PersonEntity { Name = "Карасевич, Тарас", Number = "#23", Position = "ГК", OpponentId = opponent3.Id },
                        new PersonEntity { Name = "Приходько, Степан", Number = "#1", Position = "ГК", OpponentId = opponent3.Id },
                        new PersonEntity { Name = "Малинович, Сергій", Number = "#95", Position = "ЗАХ", OpponentId = opponent3.Id },
                        new PersonEntity { Name = "Голуб, Андрій", Number = "#33", Position = "ЗАХ", OpponentId = opponent3.Id },
                        new PersonEntity { Name = "Полюхович, Роман", Number = "#3", Position = "ЗАХ", OpponentId = opponent3.Id },
                        new PersonEntity { Name = "Кротевич, Олександр", Number = "#67", Position = "ЗАХ", OpponentId = opponent3.Id },
                        new PersonEntity { Name = "Овчаренко, Денис", Number = "#57", Position = "ЗАХ", OpponentId = opponent3.Id },
                        new PersonEntity { Name = "Кухотко, Олександр", Number = "#71", Position = "ЗАХ", OpponentId = opponent3.Id },
                        new PersonEntity { Name = "Стадник, Станіслав", Number = "#17", Position = "ЗАХ", OpponentId = opponent3.Id },
                        new PersonEntity { Name = "Маринич, Дмитро", Number = "#29", Position = "ПЗХ", OpponentId = opponent3.Id },
                        new PersonEntity { Name = "Шаповал, Ігор", Number = "#10", Position = "ПЗХ", OpponentId = opponent3.Id },
                        new PersonEntity { Name = "Станько, Ігор", Number = "#6", Position = "ПЗХ", OpponentId = opponent3.Id },
                        new PersonEntity { Name = "Тарасюк, Вадим", Number = "#87", Position = "ПЗХ", OpponentId = opponent3.Id }
                    };
                    await context.Persons.AddRangeAsync(persons3);
                    await context.SaveChangesAsync();

                    var opponent4 = new OpponentEntity() { Name = "Оболонь", CountryCode = "UA", SportMatchId = match2.Id };
                    await context.Opponents.AddAsync(opponent4);
                    await context.SaveChangesAsync();

                    var persons4 = new List<PersonEntity>()
                    {
                        new PersonEntity { Name = "Головаченко, Микола", Number = "#47", Position = "ГК", OpponentId = opponent4.Id },
                        new PersonEntity { Name = "Карасевич, Тарас", Number = "#23", Position = "ГК", OpponentId = opponent4.Id },
                        new PersonEntity { Name = "Приходько, Степан", Number = "#1", Position = "ГК", OpponentId = opponent4.Id },
                        new PersonEntity { Name = "Малинович, Сергій", Number = "#95", Position = "ЗАХ", OpponentId = opponent4.Id },
                        new PersonEntity { Name = "Голуб, Андрій", Number = "#33", Position = "ЗАХ", OpponentId = opponent4.Id },
                        new PersonEntity { Name = "Полюхович, Роман", Number = "#3", Position = "ЗАХ", OpponentId = opponent4.Id },
                        new PersonEntity { Name = "Кротевич, Олександр", Number = "#67", Position = "ЗАХ", OpponentId = opponent4.Id },
                        new PersonEntity { Name = "Овчаренко, Денис", Number = "#57", Position = "ЗАХ", OpponentId = opponent4.Id },
                        new PersonEntity { Name = "Кухотко, Олександр", Number = "#71", Position = "ЗАХ", OpponentId = opponent4.Id },
                        new PersonEntity { Name = "Стадник, Станіслав", Number = "#17", Position = "ЗАХ", OpponentId = opponent4.Id },
                        new PersonEntity { Name = "Маринич, Дмитро", Number = "#29", Position = "ПЗХ", OpponentId = opponent4.Id },
                        new PersonEntity { Name = "Шаповал, Ігор", Number = "#10", Position = "ПЗХ", OpponentId = opponent4.Id },
                        new PersonEntity { Name = "Станько, Ігор", Number = "#6", Position = "ПЗХ", OpponentId = opponent4.Id },
                        new PersonEntity { Name = "Тарасюк, Вадим", Number = "#87", Position = "ПЗХ", OpponentId = opponent4.Id }
                    };
                    await context.Persons.AddRangeAsync(persons4);
                    await context.SaveChangesAsync();
                    #endregion

                    #region Match3
                    var match3 = new SportMatchEntity() { Name = "Match3", SportEventId = event1.Id, DateStart = DateTime.UtcNow.AddDays(2), DateEnd = DateTime.UtcNow.AddDays(3) };
                    await context.SportMatches.AddAsync(match3);
                    await context.SaveChangesAsync();

                    var opponent5 = new OpponentEntity() { Name = "Рух Львів", CountryCode = "UA", SportMatchId = match3.Id };
                    await context.Opponents.AddAsync(opponent5);
                    await context.SaveChangesAsync();

                    var persons5 = new List<PersonEntity>()
                    {
                        new PersonEntity { Name = "Головаченко, Микола", Number = "#47", Position = "ГК", OpponentId = opponent5.Id },
                        new PersonEntity { Name = "Карасевич, Тарас", Number = "#23", Position = "ГК", OpponentId = opponent5.Id },
                        new PersonEntity { Name = "Приходько, Степан", Number = "#1", Position = "ГК", OpponentId = opponent5.Id },
                        new PersonEntity { Name = "Малинович, Сергій", Number = "#95", Position = "ЗАХ", OpponentId = opponent5.Id },
                        new PersonEntity { Name = "Голуб, Андрій", Number = "#33", Position = "ЗАХ", OpponentId = opponent5.Id },
                        new PersonEntity { Name = "Полюхович, Роман", Number = "#3", Position = "ЗАХ", OpponentId = opponent5.Id },
                        new PersonEntity { Name = "Кротевич, Олександр", Number = "#67", Position = "ЗАХ", OpponentId = opponent5.Id },
                        new PersonEntity { Name = "Овчаренко, Денис", Number = "#57", Position = "ЗАХ", OpponentId = opponent5.Id },
                        new PersonEntity { Name = "Кухотко, Олександр", Number = "#71", Position = "ЗАХ", OpponentId = opponent5.Id },
                        new PersonEntity { Name = "Стадник, Станіслав", Number = "#17", Position = "ЗАХ", OpponentId = opponent5.Id },
                        new PersonEntity { Name = "Маринич, Дмитро", Number = "#29", Position = "ПЗХ", OpponentId = opponent5.Id },
                        new PersonEntity { Name = "Шаповал, Ігор", Number = "#10", Position = "ПЗХ", OpponentId = opponent5.Id },
                        new PersonEntity { Name = "Станько, Ігор", Number = "#6", Position = "ПЗХ", OpponentId = opponent5.Id },
                        new PersonEntity { Name = "Тарасюк, Вадим", Number = "#87", Position = "ПЗХ", OpponentId = opponent5.Id }
                    };
                    await context.Persons.AddRangeAsync(persons5);
                    await context.SaveChangesAsync();

                    var opponent6 = new OpponentEntity() { Name = "Зоря Луганськ", CountryCode = "UA", SportMatchId = match3.Id };
                    await context.Opponents.AddAsync(opponent6);
                    await context.SaveChangesAsync();

                    var persons6 = new List<PersonEntity>()
                    {
                        new PersonEntity { Name = "Головаченко, Микола", Number = "#47", Position = "ГК", OpponentId = opponent6.Id },
                        new PersonEntity { Name = "Карасевич, Тарас", Number = "#23", Position = "ГК", OpponentId = opponent6.Id },
                        new PersonEntity { Name = "Приходько, Степан", Number = "#1", Position = "ГК", OpponentId = opponent6.Id },
                        new PersonEntity { Name = "Малинович, Сергій", Number = "#95", Position = "ЗАХ", OpponentId = opponent6.Id },
                        new PersonEntity { Name = "Голуб, Андрій", Number = "#33", Position = "ЗАХ", OpponentId = opponent6.Id },
                        new PersonEntity { Name = "Полюхович, Роман", Number = "#3", Position = "ЗАХ", OpponentId = opponent6.Id },
                        new PersonEntity { Name = "Кротевич, Олександр", Number = "#67", Position = "ЗАХ", OpponentId = opponent6.Id },
                        new PersonEntity { Name = "Овчаренко, Денис", Number = "#57", Position = "ЗАХ", OpponentId = opponent6.Id },
                        new PersonEntity { Name = "Кухотко, Олександр", Number = "#71", Position = "ЗАХ", OpponentId = opponent6.Id },
                        new PersonEntity { Name = "Стадник, Станіслав", Number = "#17", Position = "ЗАХ", OpponentId = opponent6.Id },
                        new PersonEntity { Name = "Маринич, Дмитро", Number = "#29", Position = "ПЗХ", OpponentId = opponent6.Id },
                        new PersonEntity { Name = "Шаповал, Ігор", Number = "#10", Position = "ПЗХ", OpponentId = opponent6.Id },
                        new PersonEntity { Name = "Станько, Ігор", Number = "#6", Position = "ПЗХ", OpponentId = opponent6.Id },
                        new PersonEntity { Name = "Тарасюк, Вадим", Number = "#87", Position = "ПЗХ", OpponentId = opponent6.Id }
                    };
                    await context.Persons.AddRangeAsync(persons6);
                    await context.SaveChangesAsync();
                    #endregion

                    #endregion

                    #region Event2


                    var event2 = new SportEventEntity() { Name = "УКРАЇНА | КУБОК УКРАЇНИ", DateStart = DateTime.UtcNow.AddDays(2), DateEnd = DateTime.UtcNow.AddDays(30), Status = "Waiting" };
                    


                    #endregion

                    #region Event3


                    var event3 = new SportEventEntity() { Name = "ЄВРОПА | ЛІГА ЧЕМПІОНІВ. КВАЛІФІКАЦІЯ", DateStart = DateTime.UtcNow.AddDays(2), DateEnd = DateTime.UtcNow.AddDays(30), Status = "Waiting" };

                   


                    #endregion




                    


                    await context.SaveChangesAsync();
                }              
            }
        }
    }
}
