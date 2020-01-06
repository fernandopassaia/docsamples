class JournalEntry{
    constructor(days, member, books){
        this.DueDate = new Date().getDate()+days;
        this.CheckoutDate = new Date();        
        this.Member = member;
        this.ActualCheckinDate = new Date();
        this.CheckOurDate = new Date();
        this.Books = books;
    }
    
    CheckIn(actualCheckinDate){
        this.ActualCheckinDate = actualCheckinDate;
    }    
}

class Book{
    constructor(isbn, title, authors, price, publisher, year){
        this.ISBN = isbn;
        this.Title = title;
        this.Authors = authors;
        this.Price = price;
        this.Publisher = publisher;
        this.Year = year;
    }    
}

class Member{
    constructor(lastName, firstName, dateOfJoining, dateOfBirth){
        this.LastName = lastName;
        this.FirstName = firstName;
        this.DateOfJoining = dateOfJoining;
        this.DateOfBirth = dateOfBirth;
    }
}

//this is the Principal class
class Journal{    
    constructor(){
        this.JournalEntries = [];
    }
    
    AddJournalEntry(JournalEntry){
        this.JournalEntries.push(JournalEntry);
    }

    JournalEntriesLate(){
        return this.JournalEntries.filter((item) => {
            item.ActualCheckinDate > item.DueDate
        });
    }

    PrintMembersWhoAreLate(){
        var result = this.JournalEntriesLate();        
        for(name in result)
        {
            //Format here Name + Info...
        }
    }

    PrintLateFeesForAllMembers(){
    }
}

//Part where i will create my Objects and will use console to print results
var Books = [
    new Book('1', 'C# OReilly','Anders Helsberg', 10, 'Writer1', 2014),
    new Book('2', 'JS Mastering','SomCrackJS', 15, 'Writer2', 2015),
    new Book('3', 'Python','Python Team', 20, 'Writer3', 2016),
    new Book('4', 'KryptonSuite','OldStuff', 25, 'Writer4', 2017),
    new Book('5', 'Walking Over Europe','A Traveller', 30, 'Writer5', 2018)
];

var Members = [
    new Member("Ístvan", "László", Date.now, Date.now),
    new Member("Erszébet", "Ridovics", Date.now, Date.now)
];

var entry1 = new JournalEntry(10, Members[0], Books[ Books[0], Books[2], Books[4]]);
var entry2 = new JournalEntry(10, Members[1], Books[ Books[1], Books[3]]);

entry1.CheckIn(new Date());
entry2.CheckIn(new Date());

var journal = new Journal();
journal.AddJournalEntry(entry1);
journal.AddJournalEntry(entry2);
journal.PrintMembersWhoAreLate();
journal.PrintLateFeesForAllMembers();