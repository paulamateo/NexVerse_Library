﻿using LibraryApp.Presentation;
using LibraryApp.Data;
using LibraryApp.Business;

var _libraryRepository = new LibraryRepository();
var _libraryService = new LibraryService(_libraryRepository);

Menu _menu = new Menu(_libraryService, new Style());

bool exit = false;
_menu.DisplayWelcome();

while (!exit) {
    _menu.DisplayMainMenu();
    Console.WriteLine("\nELIGE UNA OPCIÓN:");
    try {
        var option = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("");
        switch(option) {
            case 1:
                var (name, lastname, email, password, phoneNumber) = _menu.DisplayPanelforCreateAccount();
                bool userCreated = _libraryService.CreateUser(name, lastname, email, password, phoneNumber);
                
                if (userCreated) {
                    Console.WriteLine("\nCuenta creada exitosamente.\n");
                    _libraryService.SetCurrentUser(email);
                    _menu.DisplaySecondMenu();
                }else {
                    Console.WriteLine("\nEl correo electrónico ya está asociado a una cuenta existente.\n");
                    _menu.DisplayMainMenu();
                }
                break;
            case 2:
                var (emailLogin, passwordLogin) = _menu.DisplayPanelforLogin();
                bool isAuthenticated = _libraryService.AuthenticateUser(emailLogin, passwordLogin);

                if (isAuthenticated) {
                    _libraryService.SetCurrentUser(emailLogin);
                    _menu.DisplaySecondMenu();
                }else {
                    Console.WriteLine("\nInicio de sesión fallido. Comprueba que la contraseña o el correo sean correctos.\n");
                }
                break;
            case 3:
                exit = true;
                _menu.DisplayFarewell();
                break;
            default:
                Console.WriteLine($"\nLa opción {option} no está en el menú.\n");
                break;
        }
    }catch (FormatException) {
        Console.WriteLine($"\nError de formato. Debes introducir un carácter válido.\n");
    }
}