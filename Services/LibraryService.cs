using System;
using System.Collections.Generic;
using CodeLibrary.Models;

namespace CodeLibrary.Services
{
  class LibraryService
  {
    private List<Book> Books { get; set; }

    internal string GetBooks(bool available)
    {
      string list = "";
      var filtered = Books.FindAll(b => b.IsAvailable == available);
      if (available)
      {
        list += "Available Books: \n";
      }
      else
      {
        list += "Checked Out Books: \n";
      }
      if (filtered.Count == 0)
      {
        list = "There Are No " + list;
      }
      for (int i = 0; i < filtered.Count; i++)
      {
        var book = filtered[i];
        if (book.IsAvailable == available)
        {
          list += $"{i + 1}. {book.Title} - by {book.Author}\n";
        }
      }
      return list;
    }

    internal string ToggleStatus(int selection, bool available)
    {
      var books = Books.FindAll(b => b.IsAvailable == available);
      if (selection < books.Count)
      {
        books[selection].IsAvailable = !books[selection].IsAvailable;
        return available ? "Enjoy your Book" : "Thanks for the Return";
      }
      return "Invalid Input, please provide a number listed";
    }
    internal void Add(Book newBook)
    {
      Books.Add(newBook);
    }

    public LibraryService()
    {
      Books = new List<Book>(){
        new Book("Where the Sidewalk Ends", "Shel Silverstein", "You'll meet a boy who turns into a TV set, and a girl who eats a whale. The Unicorn and the Bloath live there, and so does Sarah Cynthia Sylvia Stout who will not take the garbage out. It is a place where you wash your shadow and plant diamond gardens, a place where shoes fly, sisters are auctioned off, and crocodiles go to the dentist."),
        new Book("The Hobbit", "J.R.R Tolkien", "Bilbo Baggins is a hobbit who enjoys a comfortable, unambitious life, rarely traveling any farther than his pantry or cellar. But his contentment is disturbed when the wizard Gandalf and a company of dwarves arrive on his doorstep one day to whisk him away on an adventure."),
        new Book("Jurassic Park", "Michael Crichton", "An astonishing technique for recovering and cloning dinosaur DNA has been discovered. Now humankind’s most thrilling fantasies have come true. Creatures extinct for eons roam Jurassic Park with their awesome presence and profound mystery, and all the world can visit them—for a price"),
        new Book("The Gunslinger", "Stephen King", "Roland, the world's last gunslinger, tracks an enigmatic Man in Black toward a forbidding dark tower, fighting forces both mortal and other-worldly on his quest.")
      };
    }

    internal int FindIndexByTitle(string title)
    {
      return Books.FindIndex(b => b.Title == title);
    }

    internal void Remove(int index)
    {
      Books.RemoveAt(index);
    }
  }
}