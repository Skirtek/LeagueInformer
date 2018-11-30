﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LeagueInformer.Resources {
    using System;
    
    
    /// <summary>
    ///   Klasa zasobu wymagająca zdefiniowania typu do wyszukiwania zlokalizowanych ciągów itd.
    /// </summary>
    // Ta klasa została automatycznie wygenerowana za pomocą klasy StronglyTypedResourceBuilder
    // przez narzędzie, takie jak ResGen lub Visual Studio.
    // Aby dodać lub usunąć składową, edytuj plik ResX, a następnie ponownie uruchom narzędzie ResGen
    // z opcją /str lub ponownie utwórz projekt VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class AppResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AppResources() {
        }
        
        /// <summary>
        /// Zwraca buforowane wystąpienie ResourceManager używane przez tę klasę.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("LeagueInformer.Resources.AppResources", typeof(AppResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Przesłania właściwość CurrentUICulture bieżącego wątku dla wszystkich
        ///   przypadków przeszukiwania zasobów za pomocą tej klasy zasobów wymagającej zdefiniowania typu.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Naciśnij ENTER, aby zakończyć działanie programu.
        /// </summary>
        internal static string Common_ExitApp {
            get {
                return ResourceManager.GetString("Common_ExitApp", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Wybrana opcja jest niedostępna.
        /// </summary>
        internal static string Common_OptionIsNotAvailable {
            get {
                return ResourceManager.GetString("Common_OptionIsNotAvailable", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu \nSpróbuj ponownie później :).
        /// </summary>
        internal static string Common_TryAgainLater {
            get {
                return ResourceManager.GetString("Common_TryAgainLater", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Wystąpił błąd pobierania danych z serwerów Riot Games,spróbuj ponownie później.
        /// </summary>
        internal static string Error_DownloadingData {
            get {
                return ResourceManager.GetString("Error_DownloadingData", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Nie znaleziono przywoływacza o podanym nicku.
        /// </summary>
        internal static string Error_PlayerNotFound {
            get {
                return ResourceManager.GetString("Error_PlayerNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Wybrany gracz nie posiada historii gier.
        /// </summary>
        internal static string Error_PlayerWithoutGamesHistory {
            get {
                return ResourceManager.GetString("Error_PlayerWithoutGamesHistory", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Ups!\nAplikacja nie może przetworzyć danych.
        /// </summary>
        internal static string Error_RequestAppError {
            get {
                return ResourceManager.GetString("Error_RequestAppError", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Upłynął czas przetwarzania żądania.\nPrawdopodobnie straciłeś połączenie z internetem lub serwery Riot nie odpowiadają.{0}.
        /// </summary>
        internal static string Error_RequestTimedOut {
            get {
                return ResourceManager.GetString("Error_RequestTimedOut", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Serwery Riot Games nie odpowiadają..
        /// </summary>
        internal static string Error_RiotServersAreDown {
            get {
                return ResourceManager.GetString("Error_RiotServersAreDown", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Wystąpił nieznany błąd,spróbuj ponownie później.
        /// </summary>
        internal static string Error_Undefined {
            get {
                return ResourceManager.GetString("Error_Undefined", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Wybierz interesującą Cię funkcję:.
        /// </summary>
        internal static string Main_ChooseFunction {
            get {
                return ResourceManager.GetString("Main_ChooseFunction", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Aby używać aplikacji konieczne jest połączenie z internetem.
        ///Do zobaczenia!.
        /// </summary>
        internal static string Main_NoInternetConnection {
            get {
                return ResourceManager.GetString("Main_NoInternetConnection", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu 4. Wyjdź.
        /// </summary>
        internal static string Main_Quit {
            get {
                return ResourceManager.GetString("Main_Quit", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Witaj w League Informer!.
        /// </summary>
        internal static string Main_WelcomeUser {
            get {
                return ResourceManager.GetString("Main_WelcomeUser", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu 3. O aplikacji.
        /// </summary>
        internal static string MainManu_AboutApp {
            get {
                return ResourceManager.GetString("MainManu_AboutApp", resourceCulture);
            }
        }
    }
}
