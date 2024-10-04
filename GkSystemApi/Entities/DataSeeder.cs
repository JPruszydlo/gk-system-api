using gk_system_api.Utils;

namespace gk_system_api.Entities
{
    public class DataSeeder
    {
        private readonly GkSystemDbContext _db;
        public DataSeeder(GkSystemDbContext db)
        {
            _db = db;
        }
        public void SeedData()
        {
            try
            {
                if (!_db.Database.CanConnect())
                    return;
                if (!_db.GeneralConfig.Any())
                {
                    _db.GeneralConfig.AddRange(GeneralConfig);
                    _db.SaveChanges();
                }
                if (!_db.CarouselConfig.Any())
                {
                    _db.CarouselConfig.AddRange(CarouselConfig);
                    _db.SaveChanges();
                }
                if (!_db.References.Any())
                {
                    _db.References.AddRange(References);
                    _db.SaveChanges();
                }
                if (!_db.Realisations.Any())
                {
                    _db.Realisations.AddRange(Realisations);
                    _db.SaveChanges();
                }
                if (!_db.Offers.Any())
                {
                    _db.Offers.AddRange(Offers);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public List<Offer> Offers
        {
            get => new List<Offer>
            {
                new Offer
                {
                    CreateAt = 1724358536245,
                    Available = true,
                    Name = "Dom na Pogodnej",
                    Name2 = "jednorodzinny z poddaszem użytkowym",
                    Description = "Nieruchomość obejmująca działkę o powierzchni wynoszącej 1539 m2, położona jest w Jaworznie (dzielnica Bielany) zabudowana budynkiem mieszkalnym jednorodzinnym w zabudowie bliźniaczej. Wokół domu znajduje się pięknie zagospodarowany ogród z dużą ilością drzew i krzewów co gwarantuje prywatność. Możliwość wydzielenia kilku stref relaksu. W odległości ok. 450 m od nieruchomości zlokalizowany jest wjazd na drogę krajową nr 79, nieco dalej w odległości ok. 6,2 km znajduje się węzeł komunikacyjny łączący drogę krajową nr 79 z autostradą A4 + drogą ekspresową S1. W pobliżu znajdują się również przystanki autobusowe. Bliska lokalizacja sklepów, przedszkola, szkoły.",
                    OfferVisualisations = new List<OfferVisualisations>
                    {
                        new OfferVisualisations { Image = "Projekt 1/2.jpg" },
                        new OfferVisualisations { Image = "Projekt 1/1.jpg" }
                    },
                    OfferPlans = new List<OfferPlan>
                    {
                        new OfferPlan
                        {
                            FloorName = "Parter",
                            Image = "Projekt 1/plan1.jpg",
                            OfferPlanParams = new List<OfferPlanParams>
                            {
                                new OfferPlanParams
                                {
                                    Name = "Salon",
                                    Value = "39.4 m2"
                                },
                                new OfferPlanParams
                                {
                                    Name = "Łazienka",
                                    Value = "5.2 m2"
                                },
                                new OfferPlanParams
                                {
                                    Name = "Kuchnia",
                                    Value = "18.5 m2"
                                },
                                new OfferPlanParams
                                {
                                    Name = "Gabinet",
                                    Value = "12.2 m2"
                                },
                                new OfferPlanParams
                                {
                                    Name = "Garaż",
                                    Value = "38 m2"
                                },
                                new OfferPlanParams
                                {
                                    Name = "Spiżarnia",
                                    Value = "3.3 m2"
                                }
                            }
                        },
                        new OfferPlan
                        {
                            FloorName = "Poddasze",
                            Image = "Projekt 1/plan2.jpg",
                            OfferPlanParams = new List<OfferPlanParams>
                            {
                                new OfferPlanParams
                                {
                                    Name = "Łazienka",
                                    Value = "12.2 m2"
                                }, 
                                new OfferPlanParams
                                {
                                    Name = "Pokój 1",
                                    Value = "12.2 m2"
                                },
                                new OfferPlanParams
                                {
                                    Name = "Pokój 2",
                                    Value = "12.2 m2"
                                },
                                new OfferPlanParams
                                {
                                    Name = "Pokój 3",
                                    Value = "12.2 m2"
                                },
                                new OfferPlanParams
                                {
                                    Name = "Garderoba",
                                    Value = "12.2 m2"
                                },
                            }
                        }
                    },
                    OfferParams = new List<OfferParams>
                    {
                        new OfferParams
                        {
                            Name = "Rodzaj zabudowy",
                            Value = "bliźniak"
                        },
                        new OfferParams
                        {
                            Name = "Powierzchnia użytkowa",
                            Value = "129.9 m2"
                        },
                        new OfferParams
                        {
                            Name = "Stan wykończenia",
                            Value = "surowy zamknięty"
                        },
                        new OfferParams
                        {
                            Name = "Powierzchnia działki",
                            Value = "854 m2"
                        }
                    }
                }, new Offer
                {
                    CreateAt = 1724358576749,
                    Available = false,
                    Name = "Dom pod lasem",
                    Name2 = "parterowy z garażem dwustanowiskowym",
                    Description = "Nieruchomość obejmująca działkę o powierzchni wynoszącej 1539 m2, położona jest w Jaworznie (dzielnica Bielany) zabudowana budynkiem mieszkalnym jednorodzinnym w zabudowie bliźniaczej. Wokół domu znajduje się pięknie zagospodarowany ogród z dużą ilością drzew i krzewów co gwarantuje prywatność. Możliwość wydzielenia kilku stref relaksu. W odległości ok. 450 m od nieruchomości zlokalizowany jest wjazd na drogę krajową nr 79, nieco dalej w odległości ok. 6,2 km znajduje się węzeł komunikacyjny łączący drogę krajową nr 79 z autostradą A4 + drogą ekspresową S1. W pobliżu znajdują się również przystanki autobusowe. Bliska lokalizacja sklepów, przedszkola, szkoły.",
                    OfferVisualisations = new List<OfferVisualisations>
                    {
                        new OfferVisualisations { Image = "header_1.jpg" },
                        new OfferVisualisations { Image = "header_2.jpg" },
                        new OfferVisualisations { Image = "header_3.jpg" },
                    },
                    OfferPlans = new List<OfferPlan>
                    {
                        new OfferPlan
                        {
                            FloorName = "Parter",
                            Image = "Projekt 1/plan1.jpg",
                            OfferPlanParams = new List<OfferPlanParams>
                            {
                                new OfferPlanParams
                                {
                                    Name = "Salon",
                                    Value = "39.4 m2"
                                },
                                new OfferPlanParams
                                {
                                    Name = "Łazienka",
                                    Value = "5.2 m2"
                                },
                                new OfferPlanParams
                                {
                                    Name = "Kuchnia",
                                    Value = "18.5 m2"
                                },
                                new OfferPlanParams
                                {
                                    Name = "Gabinet",
                                    Value = "12.2 m2"
                                },
                                new OfferPlanParams
                                {
                                    Name = "Garaż",
                                    Value = "38 m2"
                                },
                                new OfferPlanParams
                                {
                                    Name = "Spiżarnia",
                                    Value = "3.3 m2"
                                }
                            }
                        },
                        new OfferPlan
                        {
                            FloorName = "Poddasze",
                            Image = "Projekt 1/plan2.jpg",
                            OfferPlanParams = new List<OfferPlanParams>
                            {
                                new OfferPlanParams
                                {
                                    Name = "Łazienka",
                                    Value = "12.2 m2"
                                },
                                new OfferPlanParams
                                {
                                    Name = "Pokój 1",
                                    Value = "12.2 m2"
                                },
                                new OfferPlanParams
                                {
                                    Name = "Pokój 2",
                                    Value = "12.2 m2"
                                },
                                new OfferPlanParams
                                {
                                    Name = "Pokój 3",
                                    Value = "12.2 m2"
                                },
                                new OfferPlanParams
                                {
                                    Name = "Garderoba",
                                    Value = "12.2 m2"
                                },
                            }
                        }
                    },
                    OfferParams = new List<OfferParams>
                    {
                        new OfferParams
                        {
                            Name = "Rodzaj zabudowy",
                            Value = "wolnostojący"
                        },
                        new OfferParams
                        {
                            Name = "Powierzchnia użytkowa",
                            Value = "96.6 m2"
                        },
                        new OfferParams
                        {
                            Name = "Stan wykończenia",
                            Value = "developerski"
                        },
                        new OfferParams
                        {
                            Name = "Powierzchnia działki",
                            Value = "682 m2"
                        }
                    }
                }
            };
        }

        public List<Realisation> Realisations
        {
            get => new List<Realisation>
            {
                new Realisation
                {
                    Name = "Po prostu dom",
                    RealisationImages = new List<RealisationImage>
                    {
                        new RealisationImage{ ImageSrc = "construction-site-592458_1920.jpg", IsFavourite = true},
                        new RealisationImage{ ImageSrc = "hardhat-4274430_1920.jpg", IsFavourite = true},
                        new RealisationImage{ ImageSrc = "header_1.jpg", IsFavourite = true},
                        new RealisationImage{ ImageSrc = "new-home-1664284_1920.jpg", IsFavourite = true},
                        new RealisationImage{ ImageSrc = "header_2.jpg", IsFavourite = true}
                    }
                },
                new Realisation
                {
                    Name = "Dom numer 2",
                    RealisationImages = new List<RealisationImage>
                    {
                        new RealisationImage{ ImageSrc = "header_1.jpg", IsFavourite = false},
                        new RealisationImage{ ImageSrc = "header_2.jpg", IsFavourite = false},
                        new RealisationImage{ ImageSrc = "header_3.jpg", IsFavourite = true},
                    }
                }
            };
        }
        public List<Reference> References
        {
            get => new List<Reference>
            {
                new Reference{ CustomerName = "Klient 1", Image = "", Text = "Lorem ipsum dolor sit, amet consectetur adipisicing elit. Totam esse, tempora atque obcaecati molestias solutaeius accusantium adipisci repudiandae officia aperiam unde, nisi quaerat dolor blanditiis ratione tempore."},
                new Reference{ CustomerName = "Klient 2", Image = "", Text = "Lorem ipsum dolor sit, amet consectetur adipisicing elit. Totam esse, tempora atque obcaecati molestias solutaeius accusantium adipisci repudiandae officia aperiam unde, nisi quaerat dolor blanditiis ratione tempore."},
                new Reference{ CustomerName = "Klient 3", Image = "referencje/1.jpg", Text = ""}
            };
        }
        public List<CarouselConfig> CarouselConfig
        {
            get => new List<CarouselConfig>
            {
                new CarouselConfig{SubPage = "home", Image = "header_1.jpg", ContentTitle = "Budowa domów jednorodzinnych", ContentText = "Perfekcyjnie wykonujemy zlecenia korzystając materiałów dostępnych na rynku."},
                new CarouselConfig{SubPage = "home", Image = "header_2.jpg", ContentTitle = "Szeroki zakres prac", ContentText = "Realizujemy prace od fundamentów aż po dach a także wykończenie wnętrza do stanu deweloperskiego lub pod klucz."},
                new CarouselConfig{SubPage = "home", Image = "header_3.jpg", ContentTitle = "Atrakcyjne ceny", ContentText = "Zlecenia wykonujemy profesjonalnie i w korzystnych cenach. Warunki zlecenia ustalamy indywidualnie"},
                new CarouselConfig{SubPage = "about-us", Image = "hardhat-4274430_1920.jpg", ContentTitle = "", ContentText = ""},
                new CarouselConfig{SubPage = "for-sell", Image = "hardhat-4274430_1920.jpg", ContentTitle = "", ContentText = ""},
                new CarouselConfig{SubPage = "for-sell-details", Image = "hardhat-4274430_1920.jpg", ContentTitle = "", ContentText = ""},
                new CarouselConfig{SubPage = "realisations", Image = "hardhat-4274430_1920.jpg", ContentTitle = "", ContentText = ""},
                new CarouselConfig{SubPage = "references", Image = "hardhat-4274430_1920.jpg", ContentTitle = "", ContentText = ""},
                new CarouselConfig{SubPage = "contact", Image = "hardhat-4274430_1920.jpg", ContentTitle = "", ContentText = ""}
            };
        }

        public List<GeneralConfig> GeneralConfig
        {
            get => new List<GeneralConfig>()
            {
                new GeneralConfig{ConfigGroup = ConfigGroup.Contact, Name = "phone", Value = "+48 693 326 967"},
                new GeneralConfig{ConfigGroup = ConfigGroup.Contact, Name = "street", Value = "Szybowa"},
                new GeneralConfig{ConfigGroup = ConfigGroup.Contact, Name = "postCode", Value = "41-710"},
                new GeneralConfig{ConfigGroup = ConfigGroup.Contact, Name = "city", Value = "Ruda Śląska"},
                new GeneralConfig{ConfigGroup = ConfigGroup.Contact, Name = "district", Value = "Śląskie"},
                new GeneralConfig{ConfigGroup = ConfigGroup.Contact, Name = "houseNumber", Value = "6"},
                new GeneralConfig{ConfigGroup = ConfigGroup.Contact, Name = "flatNumber", Value = "7"},
                new GeneralConfig{ConfigGroup = ConfigGroup.Contact, Name = "email", Value = "biuro@gk-system.com.pl"},
                new GeneralConfig{ConfigGroup = ConfigGroup.Undefined, Name = "companyName", Value = "GK-System"},
                new GeneralConfig{ConfigGroup = ConfigGroup.Undefined, Name = "ceoName", Value = "Kazimierz Gromala"},
                new GeneralConfig{ConfigGroup = ConfigGroup.Undefined, Name = "privatePolicyCheckbox", Value = "Wyrażam zgodę na przetwarzanie moich danych osobowych przez firmę GK-System Kazimierz Gromala w celu i w zakresie niezbędnym do realizacji obsługi niniejszego zgłoszenia. Zapoznałem się z treścią informacji o sposobie przetwarzania Moich danych osobowych."},
                new GeneralConfig{ConfigGroup = ConfigGroup.GooglMapTag, Name = "googleTag", Value = "<iframe src=\"https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2550.009844115733!2d18.851845376737554!3d50.27307480011917!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x471132d32bb3269d%3A0xb72b5634fc71c8dd!2sIgnacego%20Paderewskiego%207%2C%2041-710%20Ruda%20%C5%9Al%C4%85ska!5e0!3m2!1spl!2spl!4v1723819660401!5m2!1spl!2spl\" width=\"600\" height=\"450\" style=\"border:0;\" allowfullscreen=\"\" loading=\"lazy\" referrerpolicy=\"no-referrer-when-downgrade\"></iframe>"},
                new GeneralConfig{ConfigGroup = ConfigGroup.AboutUs, Name = "aboutUsShort", Value = "<p class=\"text-xl font-bold\">Profesjonalna Budowa Domów na Śląsku</p><br />Jesteśmy rodzinną firmą budowlaną z tradycjami, specjalizującą się w budowie domów tradycyjnych i energooszczędnych. Oferujemy kompleksowe usługi, od fundamentów po dach, w konkurencyjnych cenach. Zaufaj doświadczeniu i pasji naszego zespołu - z nami Twój dom będzie solidny, funkcjonalny i piękny. Masz pytania? zadzwoń! <b class=\"text-nowrap\">+48 693-326-967</b> chętnie odpowiemy na twoje pytania!"},
                new GeneralConfig{ConfigGroup = ConfigGroup.AboutUs, Name = "aboutUsShortImage", Value = "pexels-joaquin-carfagna-3131171-18111488.jpg"},
                new GeneralConfig{ConfigGroup = ConfigGroup.AboutUs, Name = "aboutUsLong", Value = "<p class=\"font-bold text-3xl mb-6 text-center\">Przedsiębiorstwo handlowo-usługowe GK-SYSTEM Kazimierz Gromala</p> Jesteśmy rodzinną firmą budowlaną z wieloletnim doświadczeniem, działającą na terenie Śląska. Nasz zespół składa się z doświadczonych górali, którzy od pokoleń kultywują tradycje budowlane, łącząc je z nowoczesnymi technologiami. Dzięki temu możemy zaoferować naszym klientom najwyższą jakość usług w konkurencyjnych cenach.<p class=\"font-bold text-xl my-4\">Nasze usługi</p>Specjalizujemy się w budowie domów tradycyjnych i energooszczędnych. Realizujemy kompleksowe projekty budowlane, obejmujące:<ul style = \"list-style-type: disc; padding-left: 20px; padding-top: 10px\"><li><b> Wykonanie fundamentów</b> - od tradycyjnych ław fundamentowych po nowoczesne płyty fundamentowe.</li><li><b>Murowanie ścian</b> - solidnie i precyzyjnie, zgodnie z najlepszymi praktykami budowlanymi.</li><li><b>Wykonanie stropów</b> - zapewniających trwałość i bezpieczeństwo konstrukcji.</li><li><b>Więźba dachowa i pokrycie dachów</b> - od solidnych konstrukcji po estetyczne i trwałe pokrycia dachowe.</li><li><b>Tynki wewnętrzne i zewnętrzne oraz posadzki</b> - dbamy o każdy szczegół wykończenia wnętrz i elewacji.</li><li><b>Elewacje, ocieplenia ścian i poddaszy</b> - zapewniamy energooszczędność i estetykę.</li><li><b>Budowa ogrodzenia i podjazdów</b> - kompleksowo wykańczamy otoczenie domu, dbając o jego funkcjonalność i estetykę.</li></ul><p class=\"font-bold text-xl my-4\">Dlaczego my?</p> Naszym największym atutem jest bogate doświadczenie i pasja do budownictwa.Dokładamy wszelkich starań, aby każdy projekt był realizowany zgodnie z oczekiwaniami klienta, na czas i w ustalonym budżecie.Jesteśmy dumni z tego, że wielu zadowolonych klientów poleca nas dalej, co jest dla nas najlepszą rekomendacją. Zapraszamy do kontaktu i współpracy - z nami Twoje marzenia o idealnym domu staną się rzeczywistością!"},
                new GeneralConfig{ConfigGroup = ConfigGroup.TechnicalBreak, Name = "isTechnicalBreak", Value = "1"},
            };
        }
    }
}
