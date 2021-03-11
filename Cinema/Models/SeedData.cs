using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Cinema.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cinema.Models
{
    public class SeedData
    {

        private static List<Movie> moviesList { get; set; }

        private static List<Hall> hallsList { get; set; }

        private static IServiceProvider _servise { get; set; }

        private static ILogger<Program> _logger { get; set; }

        public static void Initialize (IServiceProvider serviceProvider)
        {
            _servise = serviceProvider;
            _logger = _servise.GetRequiredService<ILogger<Program>>();
            using (var context = new MvcCinemaContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcCinemaContext>>()))
            {
                if (!context.Seat.Any())
                {
                    var seats = SeedSeats();
                    context.Seat.AddRange(seats);
                }
                if (!context.Hall.Any())
                {
                    var halls = SeedHalls();
                    context.Hall.AddRange(halls);
                }
                hallsList = context.Hall.ToList();
                if (!context.Movie.Any())
                {
                    var movies = SeedMovies();
                    context.Movie.AddRange(movies);
                }
                moviesList = context.Movie.ToList();
                if (!context.Session.Any())
                {
                    var session = SeedSessions();
                    context.Session.AddRange(session);
                }
                context.SaveChanges();
        }
    }

        private static List<Seat> SeedSeats()
        {
            List<Seat> initSeats = new List<Seat>();
            for (int i = 1; i <= 11; i++)
            {
                for (int j = 1; j <= 15; j++)
                {
                    Seat currentSeat = new Seat();
                    currentSeat.Row = i;
                    _logger.LogInformation(j, "seat number");
                    currentSeat.Number = j;
                    initSeats.Add(currentSeat);
                }
            }
            return initSeats;
        }

        private static List<Hall> SeedHalls()
        {
            List<Hall> initHalls = new List<Hall>();
            List<string> russianTrans = new List<string>() { "Первый", "Второй", "Третий", "Четвертый" };
            foreach (string number in russianTrans)
            {
                Hall hall = new Hall();
                hall.Name = number;
                initHalls.Add(hall);
            }
            return initHalls;
        }

        private static List<Movie> SeedMovies()
        {
            List<Movie> initMovies = new List<Movie>();
            initMovies.Add(
                new Movie
                {
                    Title = "Побег из Шоушенка",
                    Genre = "Драма",
                    Desc = "Бухгалтер Энди Дюфрейн обвинён в убийстве собственной жены и её любовника. Оказавшись в тюрьме под названием Шоушенк, он сталкивается с жестокостью и беззаконием, царящими по обе стороны решётки. Каждый, кто попадает в эти стены, становится их рабом до конца жизни. Но Энди, обладающий живым умом и доброй душой, находит подход как к заключённым, так и к охранникам, добиваясь их особого к себе расположения.",
                    Director = "Фрэнк Дарабонт",
                    Poster = "pobegisshoushenka.jpg",
                    Rating = "9,1",
                    ReleaseDate = DateTime.Parse("1994-10-24")
                }
            );
            initMovies.Add(
                new Movie
                {
                    Title = "Зеленая миля",
                    Genre = "Фэнтези, Драма, Криминал, Детектив",
                    Desc = "Пол Эджкомб — начальник блока смертников в тюрьме «Холодная гора», каждый из узников которого однажды проходит «зеленую милю» по пути к месту казни. Пол повидал много заключённых и надзирателей за время работы. Однако гигант Джон Коффи, обвинённый в страшном преступлении, стал одним из самых необычных обитателей блока.",
                    Director = "Фрэнк Дарабонт",
                    Poster = "zelenayamila.jpg",
                    Rating = "9",
                    ReleaseDate = DateTime.Parse("2000-04-18")
                }
            );
            initMovies.Add(
                new Movie
                {
                    Title = "Властелин колец: Возвращение Короля",
                    Genre = "фэнтези, приключения, драма",
                    Desc = "Последняя часть трилогии о Кольце Всевластия и о героях, взявших на себя бремя спасения Средиземья. Повелитель сил Тьмы Саурон направляет свои бесчисленные рати под стены Минас-Тирита, крепости Последней Надежды. Он предвкушает близкую победу, но именно это и мешает ему заметить две крохотные фигурки - хоббитов, приближающихся к Роковой Горе, где им предстоит уничтожить Кольцо Всевластия. Улыбнется ли им счастье?",
                    Director = "Питер Джексон",
                    Poster = "vlastelinkolets3.jpg",
                    Rating = "8.6",
                    ReleaseDate = DateTime.Parse("2004-01-22")
                }
            );
            initMovies.Add(
                new Movie
                {
                    Title = "Интерстеллар",
                    Genre = "фантастика, драма, приключения",
                    Desc = "Когда засуха, пыльные бури и вымирание растений приводят человечество к продовольственному кризису, коллектив исследователей и учёных отправляется сквозь червоточину (которая предположительно соединяет области пространства-времени через большое расстояние) в путешествие, чтобы превзойти прежние ограничения для космических путешествий человека и найти планету с подходящими для человечества условиями.",
                    Director = "Кристофер Нолан",
                    Poster = "interstellar.jpg",
                    Rating = "8.6",
                    ReleaseDate = DateTime.Parse("2014-11-06")
                }
            );
            initMovies.Add(
                new Movie
                {
                    Title = "Список Шиндлера",
                    Genre = "драма, биография, история, военный",
                    Desc = "Фильм рассказывает реальную историю загадочного Оскара Шиндлера, члена нацистской партии, преуспевающего фабриканта, спасшего во время Второй мировой войны почти 1200 евреев.",
                    Director = "Стивен Спилберг",
                    Poster = "spisokshindlera.jpg",
                    Rating = "8.8",
                    ReleaseDate = DateTime.Parse("1994-05-21")
                }
            );
            initMovies.Add(
                new Movie
                {
                    Title = "Форрест Гамп",
                    Genre = "драма, мелодрама, комедия, история, военный",
                    Desc = "От лица главного героя Форреста Гампа, слабоумного безобидного человека с благородным и открытым сердцем, рассказывается история его необыкновенной жизни. Фантастическим образом превращается он в известного футболиста, героя войны, преуспевающего бизнесмена.Он становится миллиардером, но остается таким же бесхитростным, глупым и добрым.Форреста ждет постоянный успех во всем, а он любит девочку, с которой дружил в детстве, но взаимность приходит слишком поздно.",
                    Director = "Роберт Земекис",
                    Poster = "forestgump.jpg",
                    Rating = "8.9",
                    ReleaseDate = DateTime.Parse("1994-06-23")
                }
            );
            initMovies.Add(
                new Movie
                {
                    Title = "Иван Васильевич меняет профессию",
                    Genre = "фантастика, комедия, приключения",
                    Desc = "Инженер-изобретатель Тимофеев сконструировал машину времени, которая соединила его квартиру с далеким шестнадцатым веком — точнее, с палатами государя Ивана Грозного. Туда-то и попадают тезка царя пенсионер-общественник Иван Васильевич Бунша и квартирный вор Жорж Милославский. На их место в двадцатом веке «переселяется» великий государь.Поломка машины приводит ко множеству неожиданных и забавных событий...",
                    Director = "Леонид Гайдай",
                    Poster = "ivanvasilievichmenyaetprof.jpg",
                    Rating = "8.8",
                    ReleaseDate = DateTime.Parse("1973-09-17")
                }
            );
            initMovies.Add(
                new Movie
                {
                    Title = "Назад в будущее",
                    Genre = "фантастика, комедия, приключения",
                    Desc = "Подросток Марти с помощью машины времени, сооружённой его другом-профессором доком Брауном, попадает из 80-х в далекие 50-е. Там он встречается со своими будущими родителями, ещё подростками, и другом-профессором, совсем молодым.",
                    Director = "Роберт Земекис",
                    Poster = "nazadvbudushee.jpg",
                    Rating = "8.6",
                    ReleaseDate = DateTime.Parse("1985-01-01")
                }
            );
            initMovies.Add(
                new Movie
                {
                    Title = "Король Лев",
                    Genre = "мультфильм, мюзикл, драма, приключения, семейный",
                    Desc = "У величественного Короля-Льва Муфасы рождается наследник по имени Симба. Уже в детстве любознательный малыш становится жертвой интриг своего завистливого дяди Шрама, мечтающего о власти. Симба познаёт горе утраты, предательство и изгнание, но в конце концов обретает верных друзей и находит любимую.Закалённый испытаниями, он в нелёгкой борьбе отвоёвывает своё законное место в «Круге жизни», осознав, что значит быть настоящим Королём.",
                    Director = "Роджер Аллерс, Роб Минкофф",
                    Poster = "korollev.jpg",
                    Rating = "8.8",
                    ReleaseDate = DateTime.Parse("1994-06-12")
                }
            );
            initMovies.Add(
                new Movie
                {
                    Title = "1+1",
                    Genre = "драма, комедия, биография",
                    Desc = "Пострадав в результате несчастного случая, богатый аристократ Филипп нанимает в помощники человека, который менее всего подходит для этой работы, – молодого жителя предместья Дрисса, только что освободившегося из тюрьмы. Несмотря на то, что Филипп прикован к инвалидному креслу, Дриссу удается привнести в размеренную жизнь аристократа дух приключений.",
                    Director = "Оливье Накаш, Эрик Толедано",
                    Poster = "odinplusodin.jpg",
                    Rating = "8.8",
                    ReleaseDate = DateTime.Parse("2011-04-26")
                }
            );
            initMovies.Add(
                new Movie
                {
                    Title = "Криминальное чтиво",
                    Genre = "триллер, комедия, криминал",
                    Desc = "Двое бандитов Винсент Вега и Джулс Винфилд ведут философские беседы в перерывах между разборками и решением проблем с должниками криминального босса Марселласа Уоллеса. В первой истории Винсент проводит незабываемый вечер с женой Марселласа Мией.Во второй рассказывается о боксёре Бутче Кулидже, купленном Уоллесом, чтобы сдать бой.В третьей истории Винсент и Джулс по нелепой случайности попадают в неприятности.",
                    Director = "Квентин Тарантино",
                    Poster = "kriminalnoechtivo.jpg",
                    Rating = "8.6",
                    ReleaseDate = DateTime.Parse("1994-05-21")
                }
            );
            initMovies.Add(
                new Movie
                {
                    Title = "Начало",
                    Genre = "фантастика, боевик, триллер, драма, детектив",
                    Desc = "Кобб – талантливый вор, лучший из лучших в опасном искусстве извлечения: он крадет ценные секреты из глубин подсознания во время сна, когда человеческий разум наиболее уязвим. Редкие способности Кобба сделали его ценным игроком в привычном к предательству мире промышленного шпионажа, но они же превратили его в извечного беглеца и лишили всего, что он когда-либо любил. И вот у Кобба появляется шанс исправить ошибки. Его последнее дело может вернуть все назад, но для этого ему нужно совершить невозможное – инициацию. Вместо идеальной кражи Кобб и его команда спецов должны будут провернуть обратное. Теперь их задача – не украсть идею, а внедрить ее. Если у них получится, это и станет идеальным преступлением. Но никакое планирование или мастерство не могут подготовить команду к встрече с опасным противником, который, кажется, предугадывает каждый их ход. Врагом, увидеть которого мог бы лишь Кобб.",
                    Director = "Кристофер Нолан",
                    Poster = "nachalo.jpg",
                    Rating = "8.7",
                    ReleaseDate = DateTime.Parse("2010-07-08")
                }
            );
            initMovies.Add(
                new Movie
                {
                    Title = "Унесённые призраками",
                    Genre = "аниме, мультфильм, фэнтези, приключения, семейный",
                    Desc = "Тихиро с мамой и папой переезжают в новый дом. Заблудившись по дороге, они оказываются в странном пустынном городе, где их ждет великолепный пир. Родители с жадностью набрасываются на еду и к ужасу девочки превращаются в свиней, став пленниками злой колдуньи Юбабы. Теперь, оказавшись одна среди волшебных существ и загадочных видений, Тихиро должна придумать, как избавить своих родителей от чар коварной старухи.",
                    Director = "Хаяо Миядзаки",
                    Poster = "unesenieprizrakami.jpg",
                    Rating = "8.4",
                    ReleaseDate = DateTime.Parse("2001-07-20")
                }
            );
            initMovies.Add(
                new Movie
                {
                    Title = "Остров проклятых",
                    Genre = "триллер, детектив, драма",
                    Desc = "Два американских судебных пристава отправляются на один из островов в штате Массачусетс, чтобы расследовать исчезновение пациентки клиники для умалишенных преступников. При проведении расследования им придется столкнуться с паутиной лжи, обрушившимся ураганом и смертельным бунтом обитателей клиники.",
                    Director = "Мартин Скорсезе",
                    Poster = "ostrovproklyatih.jpg",
                    Rating = "8.5",
                    ReleaseDate = DateTime.Parse("2010-02-13")
                }
            );
            initMovies.Add(
                new Movie
                {
                    Title = "Одержимость",
                    Genre = "драма, музыка",
                    Desc = "Эндрю мечтает стать великим. Казалось бы, вот-вот его мечта осуществится. Юношу замечает настоящий гений, дирижер лучшего в стране оркестра. Желание Эндрю добиться успеха быстро становится одержимостью, а безжалостный наставник продолжает подталкивать его все дальше и дальше – за пределы человеческих возможностей. Кто выйдет победителем из этой схватки?",
                    Director = "Дэмьен Шазелл",
                    Poster = "oderzhimost.jpg",
                    Rating = "8.3",
                    ReleaseDate = DateTime.Parse("2014-01-16")
                }
            );
            return initMovies;
        }

        private static List<Session> SeedSessions()
        {
            List<Session> initSessions = new List<Session>();

            foreach (Hall hall in hallsList)
            {
                DateTime currentShowDate = new DateTime(2021, 12, 23, 10, 0, 0);
                for (int i = 0; i < moviesList.Count * 2; i++)
                {
                    int realNumber = i;

                    if (i == moviesList.Count() || i > moviesList.Count())
                    {
                        realNumber = i - moviesList.Count();
                    }

                    Session currentSession = new Session();
                    currentSession.Movie = moviesList[realNumber];
                    currentSession.Hall = hall;
                    currentSession.ShowTime = currentShowDate;
                    initSessions.Add(currentSession);
                    currentShowDate = currentShowDate.AddHours(4);
                }
            }
            return initSessions;
        }
    }
}
