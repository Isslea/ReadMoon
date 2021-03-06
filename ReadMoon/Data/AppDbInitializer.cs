using Microsoft.EntityFrameworkCore;
using ReadMoon.Models;

namespace ReadMoon.Data;

public class AppDbInitializer
{
    private readonly AppDbContext _dbContext;

    public AppDbInitializer(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        if (_dbContext.Database.CanConnect())
        {
            if (_dbContext.Database.IsRelational())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    _dbContext.Database.Migrate();
                }
            }

            if (!_dbContext.Categories.Any())
            {
                var categories = GetCategories();
                _dbContext.Categories.AddRange(categories);
                _dbContext.SaveChanges();
            }
            
            if (!_dbContext.Publishers.Any())
            {
                var publishers = GetPublishers();
                _dbContext.Publishers.AddRange(publishers);
                _dbContext.SaveChanges();
            }
            
            if (!_dbContext.Books.Any())
            {
                var books = GetBooks();
                _dbContext.Books.AddRange(books);
                _dbContext.SaveChanges();
            }
            
            if (!_dbContext.Authors.Any())
            {
                var authors = GetAuthors();
                _dbContext.Authors.AddRange(authors);
                _dbContext.SaveChanges();
            }
            
            if (!_dbContext.BookAuthors.Any())
            {
                var bookAuthors = getBookAuthors();
                _dbContext.BookAuthors.AddRange(bookAuthors);
                _dbContext.SaveChanges();
            }
        }
    }

    private IEnumerable<Category> GetCategories()
    {
        var categories = new List<Category>()
        {
            new Category()
            {
                Name = "Fantasy i Sci-fi",
            },
            new Category()
            {
                Name = "Horror",
            },
            new Category()
            {
                Name = "Biografia"
            }
        };
        return categories;
    }
    private IEnumerable<Publisher> GetPublishers()
    {

        var publishers = new List<Publisher>()
        {
            new Publisher()
            {
                Name = "Urobors",
                ImageURL = "https://s.lubimyczytac.pl/upload/publishers/9466/9466-b.jpg",
                Description = "Uroboros wchodzi w sk??ad Grupy Wydawniczej Foksal, specjalizuje si?? w wydawaniu fantastyki dla doros??ych i m??odzie??y."
            },
            new Publisher()
            {
                Name = "Sine Qua Non",
                ImageURL = "https://s.lubimyczytac.pl/upload/publishers/8256/8256-b.jpg",
                Description = "Wydawnictwo literackie powsta??e w 2010 roku, wydaj??ce przede wszystkim ksi????ki o sporcie oraz beletrystyk??, biografi??, krymina??y czy powie??ci historyczne."
            },
            new Publisher()
            {
                Name = "Pr??szy??ski i S-ka",
                ImageURL = "https://s.lubimyczytac.pl/upload/publishers/7576/7576-b.jpg",
                Description = "PR??SZY??SKI I S-KA to marka jednego z najwi??kszych wydawnictw ksi????kowych w Polsce. Dzi??ki nim co roku na ksi??garskie p????ki trafia blisko 200 tytu????w sygnowanych logo Pr??szy??ski i S-ka, adresowanych do ludzi o bardzo r????nych zainteresowaniach, z praktycznie wszystkich grup wiekowych."
            },
            new Publisher()
            {
                Name = "??wiat Ksi????ki",
                ImageURL = "https://s.lubimyczytac.pl/upload/publishers/8831/8831-b.jpg",
                Description = "Wydawnictwo ??wiat Ksi????ki dzia??a na polskim rynku od 1994 roku. Na pocz??tku stanowi??o cz?????? Bertelsmann Media sp. z o.o., kt??rego w roku 2011 w??a??cicielem sta??a si?? grupa wydawnicza Weltbild. W roku 2013 w zwi??zku z zako??czeniem przez Weltbild dzia??alno??ci w Polsce powsta?? ??wiat Ksi????ki sp. z o.o., w kt??rym 100 % udzia????w nale??y obecnie do \"Dressler Sp. z o. o.\" Dublin Sp. komandytowo-akcyjna. Wy????cznym dystrybutorem oferty wydawniczej ??wiata Ksi????ki jest FK Olesiejuk."
            }

        };
        return publishers;
    }
    private IEnumerable<Book> GetBooks()
    {
        var books = new List<Book>()
        {

            new Book()
            {
                Title = "Dw??r cierni i r????",
                RelaseDate = new DateTime(2016, 4, 27),
                ISBN = "9788328021419",
                ImageURL = "https://s.lubimyczytac.pl/upload/books/295000/295801/611382-170x243.jpg",
                Description =
                    "Dziewi??tnastoletnia Feyre jest ??owczyni?? ??? musi polowa??, by wykarmi?? i utrzyma?? rodzin??. Podczas srogiej zimy zapuszcza si?? w poszukiwaniu zwierzyny coraz dalej, w pobli??e muru, kt??ry oddziela ludzkie ziemie od Prythian ??? krainy zamieszkanej przez czarodziejskie istoty. To rasa obdarzonych magi?? i ??miertelnie niebezpiecznych stworze??, kt??ra przed wiekami panowa??a nad ??wiatem. \n Kiedy podczas polowania Feyre zabija ogromnego wilka, nie wie, ??e tak naprawd?? strzela do faerie.Wkr??tce w drzwiach jej chaty staje pochodz??cy z Wysokiego Rodu Tamlin, w postaci z??owrogiej bestii, ????daj??c zado????uczynienia za ten czyn.Feyre musi wybra?? ??? albo zginie w nier??wnej walce, albo uda si?? razem z Tamlinem do Prythian i sp??dzi tam reszt?? swoich dni. \n Pozornie dzieli ich wszystko ??? wiek, pochodzenie, ale przede wszystkim nienawi????, kt??ra przez wieki naros??a mi??dzy ich rasami. Jednak tak naprawd?? s?? do siebie podobni o wiele bardziej, ni?? im si?? wydaje.Czy Feyre b??dzie w stanie pokona?? sw??j strach i uprzedzenia? \n Pe??na nami??tno??ci i pasji, romantyczna, brutalna i okrutna. Jedno jest pewne: Dw??r cierni i r???? to z pewno??ci?? nie cukierkowa ba???? w stylu Disneya???",
                CategoryId = 1,
                PublisherId = 1
            },
            new Book()
            {
                Title = "Czas ??niw",
                RelaseDate = new DateTime(2013, 11, 6),
                ISBN = "9788379240821",
                ImageURL = "https://s.lubimyczytac.pl/upload/books/195000/195128/190869-170x243.jpg",
                Description =
                    "Sajon.\n Nie ma bezpieczniejszego miejsca. \n ???Mo??e nie wiesz, ale ??niwa nazywane s?? te?? dobrymi zbiorami. Wci???? m??wi?? tak na ulicach: Dobre ??niwa, Zbiory Obfito??ci. Rozumiej?? to jako odbieranie nagrody, podstawowy warunek ich negocjacji z Sajonem. Ludzie oczywi??cie postrzegaj?? je inaczej. Dla nich s?? symbolem nieszcz????cia. Oznaczaj?? g????d. ??mier??. Dlatego nazywaj?? nas kosiarzami. Poniewa?? pomagamy prowadzi?? ludzi na ??mier?????. \n Rok 2059. Dziewi??tnastoletnia Paige Mahoney pracuje w kryminalnym podziemiu Sajonu Londyn. Jej szefem jest Jaxon Hall, na kt??rego zlecenie pozyskuje informacje, w??amuj??c si?? do ludzkich umys????w. Paige jest sennym w??drowcem i w ??wiecie, w kt??rym przysz??o jej ??y??, zdrad?? jest ju?? sam fakt, ??e oddycha. \n Pewnego dnia jej ??ycie zmienia si?? na zawsze. Na skutek fatalnego splotu okoliczno??ci zostaje przetransportowana do Oksfordu ??? tajemniczej kolonii karnej, kt??rej istnienie od dwustu lat utrzymywane jest w tajemnicy.Kontrol?? nad ni?? sprawuje pot????na, pochodz??ca z innego ??wiata rasa Refait??w. Paige trafia pod protektorat tajemniczego Naczelnika ??? staje si?? on jej panem i trenerem, jej naturalnym wrogiem.Je??li Paige chce odzyska?? wolno????, musi podda?? si?? zasadom panuj??cym w miejscu, w kt??rym zosta??a przeznaczona na ??mier??.",
                CategoryId = 1,
                PublisherId = 2
            },
            new Book()
            {
                Title = "Sm??tarz dla zwierzak??w",
                RelaseDate = new DateTime(2019, 4, 23),
                ISBN = "9788381690584",
                ImageURL = "https://s.lubimyczytac.pl/upload/books/4882000/4882388/732529-170x243.jpg",
                Description =
                    "Czasami martwe jest lepsze. \n Zazwyczaj przeprowadzka to pocz??tek nowego ??ycia, ale dla rodziny Creed??w sta??a si?? pocz??tkiem ich ko??ca. Mistrz horroru Stephen King zaprasza czytelnik??w na wycieczk?? do piek??a i z powrotem! \n Na ??wiecie istniej?? dobre i z??e miejsca.Nowy dom rodziny Creed??w w Ludlow by?? niew??tpliwie dobrym miejscem -przytuln??, przyjazn?? wiejsk?? przystani?? po zgie??ku i chaosie Chicago. Cudowne otoczenie Nowej Anglii, ????ki, las to idealna siedziba dla m??odego lekarza, jego ??ony, dw??jki dzieci i kota. Wspania??a praca, mili s??siedzi i droga, po kt??rej nieustannie przetaczaj?? si?? ci????ar??wki. Droga i miejsce za domem, w lesie, pe??ne wzniesionych dzieci??cymi r??kami nagrobk??w -to tam dzieci z miasteczka zakopuj?? swe martwe zwierzaki. Ci, kt??rzy nie znaj?? przesz??o??ci, zwykle j?? powtarzaj??... i nie chc?? s??ucha?? ostrze??e??.",
                CategoryId = 2,
                PublisherId = 3
            },
            new Book()
            {
                Title = "Kwiat pustyni. Z namiotu Nomad??w do Nowego Jorku",
                RelaseDate = new DateTime(2002, 1, 1),
                ISBN = "8372272808",
                ImageURL = "https://s.lubimyczytac.pl/upload/books/47000/47775/170x243.jpg",
                Description =
                    "Waris urodzi??a si?? w Somalii, w plemieniu pustynnych w??drowc??w, wychowa??a si?? po??r??d k??z, byd??a i wielb????d??w.\nW wieku 13 lat dziewcz??ta z tego plemienia s?? poddawane rytua??owi obrzezania i wydawane za m????. Jednak bohaterka ksi????ki zosta??a poddana obrzezaniu w wieku oko??o 5 lat, a mia??a by?? wydana za m???? dopiero w wieku 13 lat\nTe dwa wydarzenia zawa??y??y na ca??ym przysz??ym ??yciu Waris.",
                CategoryId = 3,
                PublisherId = 4
            },
            new Book()
                        {
                            Title = "Dw??r srebrnych p??omieni cz?????? 1",
                            RelaseDate = new DateTime(2021, 11, 10),
                            ISBN = "9788328092839",
                            ImageURL = "https://s.lubimyczytac.pl/upload/books/4965000/4965132/960268-170x243.jpg",
                            Description = "Nesta Archeron od zawsze by??a dumna, wybuchowa i niezbyt sk??onna do wybaczania. Odk??d zmuszono j?? do wej??cia do Kot??a i zosta??a Fae wbrew swojej woli, walczy o znalezienie w??asnego miejsca dla siebie w zab??jczym i niebezpiecznym ??wiecie. Co gorsza, wojna i wszystko, co w niej straci??a, wypali??y w jej duszy niezatarte pi??tno.Jedyn?? osob??, kt??ra rozpala j?? bardziej ni?? ktokolwiek inny, jest Kasjan, tajemniczy i twardy wojownik na Dworze Nocy Rhysanda i Feyry. Mi??dzy Nest?? a Kasjanem p??onie prawdziwy ogie??. ??ar jednak mo??e spali?? lub oczy??ci??.Tymczasem zdradzieckie ludzkie kr??lowe, kt??re powr??ci??y na kontynent podczas ostatniej wojny, zawar??y nowy niebezpieczny sojusz, zagra??aj??cy kruchemu pokojowi, kt??ry zapanowa?? w kr??lestwach. Klucz do powstrzymania kolejnej wojny dzier???? wsp??lnie w??a??nie Nesta i Kasjan. By jednak ocalenie pokoju by??o mo??liwe, te dwa ??ywio??y musz?? zacz???? ze sob?? wsp????pracowa??. Czy b??dzie to mo??liwe?",
                            CategoryId = 1,
                            PublisherId = 1,
                        },
                       new Book()
                        {
                            Title = "Dom ziemi i krwi. Cz?????? 1",
                            ISBN = "9788328052062",
                            RelaseDate = new DateTime(2020, 5, 20),
                            ImageURL = "https://s.lubimyczytac.pl/upload/books/4923000/4923601/809192-170x243.jpg",
                            Description = "Bryce Quinlan jest dziewczyn??, kt??ra w po??owie jest cz??owiekiem, a w po??owie Fae. W ??wiecie pe??nym magii, niebezpiecze??stw i ognistych romans??w poszukuje zemsty! Bryce Quinlan kocha swoje ??ycie. W dzie?? pracuje dla handlarza antyk??w, sprzedaj??c nie do ko??ca legalne magiczne artefakty, a noc?? imprezuje razem z przyjaci????mi, delektuj??c si?? ka??d?? przyjemno??ci?? jak?? Lunathion ??? znane r??wnie?? jako Crescent City ??? ma do zaoferowania.Ale gdy bezwzgl??dne morderstwo wstrz??sa miastem, wszystko zaczyna si?? rozpada?? ??? r??wnie?? ??wiat Bryce.",
                            CategoryId = 1,
                            PublisherId = 1,
                            
                       },
                       new Book()
                        {
                            Title = "Catwoman. Z??odziejka dusz",
                            ISBN = "9788366409415",
                            RelaseDate = new DateTime(2019, 11, 27),
                            ImageURL = "https://s.lubimyczytac.pl/upload/books/4899000/4899214/769808-170x243.jpg",
                            Description = "Selina Kyle to z??odziejka. Dwa lata po ucieczce ze slums??w Gotham City Selina Kyle wraca jako tajemnicza i bogata Holly Vanderhees. Szybko odkrywa, ??e Batman wyjecha?? z wa??n?? misj?? i Gotham City czeka, ??eby je oskuba??. Luke Fox jest bohaterem. Luke pragnie dowie????, ??e Batwing jest w stanie pom??c ludziom. Bierze na cel now?? z??odziejk??, kt??ra sprzymierzy??a si?? z Poison Ivy i Harley Quinn. Razem rozp??tuj?? chaos. Catwoman jest cwana. Mo??e zniszczy?? Batwinga. W Gotham City nie ka??dy jest tym, na kogo wygl??da. Selina prowadzi rozpaczliw?? gr?? w kotka i myszk??, zawi??zuj??c nieoczekiwane sojusze, wik??aj??c si?? w skomplikowan?? relacj?? z Batwingiem noc?? i swoim diabelnie przystojnym s??siadem Luke???iem Foxem za dnia. Jednak kiedy w??asna niebezpieczna przesz??o???? depcze jej po pi??tach, czy zdo??a przeprowadzi?? ostatni skok, kt??rego cel jest najbli??szy jej sercu?",
                            CategoryId = 1,
                            PublisherId = 1,
                            
                       }

        };
        return books;
    }
    private IEnumerable<Author> GetAuthors()
    {
        var authors = new List<Author>()
        {
            new Author()
            {
                FullName = "Sarah J. Maas",
                ImageURL = "https://s.lubimyczytac.pl/upload/authors/74122/249050-140x200.jpg",
                Bio =
                    "Ameryka??ska pisarka powie??ci fantasy. Zadebiutowa??a w 2012 powie??ci?? Szklany tron opublikowan?? przez Bloomsbury. Ksi????ka zaj????a pierwsze miejsce na li??cie bestseller??w New York Timesa.",

            },
            new Author()
            {
                FullName = "Samantha Shannon",
                ImageURL = "https://s.lubimyczytac.pl/upload/authors/81732/766091-140x200.jpg",
                Bio = "Urodzi??a si?? w zachodnim Londynie w 1991 roku. Zacz????a pisa?? w wieku pi??tnastu lat. W latach 2010???2013 studiowa??a angielsk?? literatur?? i literatur?? powszechn?? w college???u ??wi??tej Anny w Oxfordzie.",

            },
            new Author()
            {
                FullName = "Stephen King",
                ImageURL = "https://s.lubimyczytac.pl/upload/authors/13997/849814-140x200.jpg",
                Bio = "Ameryka??ski powie??ciopisarz, g????wnie literatury grozy. W przesz??o??ci publikowa?? pod pseudonimem Richard Bachman, raz jako John Swithen.",

            },
            new Author()
            {
                FullName = "Waris Dirie",
                ImageURL = "https://s.lubimyczytac.pl/upload/authors/25959/210-140x200.jpg",
                Bio = "Somalijska modelka i pisarka. Najbardziej znana, obok Iman, Somalijka na ??wiecie."
            },
            new Author()
            {
                FullName = "Cathleen Miller",
                ImageURL = "https://static.wixstatic.com/media/74fe5e_875afcbb206947a29c302a4b584059ef.jpg/v1/fill/w_451,h_548,al_c,lg_1,q_80/74fe5e_875afcbb206947a29c302a4b584059ef.webp",
                Bio = "Cathleen Miller to najlepiej sprzedaj??ca si?? na ??wiecie ameryka??ska pisarka non-fiction, kt??ra mieszka w Kalifornii."
            }
        };
        return authors;
    }
    private IEnumerable<BookAuthor> getBookAuthors()
    {
        var bookAuthors = new List<BookAuthor>()
        {
            new BookAuthor()
            {
                AuthorId = 1,
                BookId = 1
            },
            new BookAuthor()
            {
                AuthorId = 2,
                BookId = 2
            },
            new BookAuthor()
            {
                AuthorId = 3,
                BookId = 3
            },
            new BookAuthor()
            {
                AuthorId = 4,
                BookId = 4
            },
            new BookAuthor()
            {
                BookId = 4,
                AuthorId = 5
            },
            new BookAuthor()
            {
                AuthorId = 1,
                BookId = 5
            },
            new BookAuthor()
            {
                AuthorId = 1,
                BookId = 6
            },
            new BookAuthor()
            {
                AuthorId = 1,
                BookId = 7
            }
        };
        return bookAuthors;
    }
}