using System;
using CodeLibrary.Models;
using CodeLibrary.Services;

namespace CodeLibrary.Controllers
{
  class LibraryController
  {
    private LibraryService _service { get; set; } = new LibraryService();
    private bool _running { get; set; } = true;

    public void Run()
    {
      Console.Clear();
      while (_running)
      {
        Console.WriteLine("Options:\n(A)dd, (I)nfo, (C)heckout, (R)eturn, (D)elete, (Q)uit");
        GetUserInput();
      }
    }

    private void GetUserInput()
    {
      Console.WriteLine("What would you like to do:");
      string input = Console.ReadLine().ToLower();
      Console.Clear();
      switch (input)
      {
        case "add":
        case "a":
          Add();
          break;
        case "info":
        case "i":
          Info();
          break;
        case "checkout":
        case "c":
          ToggleStatus(true);
          break;
        case "return":
        case "r":
          ToggleStatus(false);
          break;
        case "delete":
        case "d":
          Delete();
          break;
        case "quit":
        case "q":
          _running = false;
          break;
        default:
          Console.WriteLine("Invalid Command");
          break;
      }
    }

    private void Add()
    {
      Console.Write("Title: ");
      string title = Console.ReadLine();
      Console.Write("Author: ");
      string author = Console.ReadLine();
      System.Console.Write("Description: ");
      string description = Console.ReadLine();
      Book newBook = new Book(title, author, description);
      _service.Add(newBook);
      System.Console.WriteLine($"Successfully added {title} to the collection");
    }

    private void Info()
    {
      throw new NotImplementedException();
    }

    private void ToggleStatus(bool available)
    {
      Console.WriteLine(_service.GetBooks(available));
      string status = available ? "Checkout" : "Return";
      Console.WriteLine($"Which book would you like to {status}?");
      string selectionStr = Console.ReadLine();
      Console.Clear();
      if (int.TryParse(selectionStr, out int selection) && selection > 0)
      {
        Console.WriteLine(_service.ToggleStatus(selection - 1, available));
        return;
      }
      Console.WriteLine("Invalid Number: Selection must be a number greater than 0");
    }



    private void Delete()
    {
      Console.Write("Enter the Title you wish to remove: ");
      string title = Console.ReadLine().ToLower();
      int index = _service.FindIndexByTitle(title);
      if (index == -1)
      {
        Console.WriteLine("No Book by that Title");
        return;
      }
      Console.WriteLine("Type 'confirm' to remove book");
      string confirm = Console.ReadLine().ToLower();
      if (confirm != "confirm")
      {
        Console.WriteLine("Invalid input, book will not be deleted");
        return;
      }
      _service.Remove(index);
      Console.WriteLine("Successfully Removed Book");
    }


  }
}
