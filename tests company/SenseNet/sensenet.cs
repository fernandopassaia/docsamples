using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
	static List<Book> Books = new List<Book>();
	static List<Member> Members = new List<Member>();
	public static void Main()
	{		
		Book book1 = new Book();
		Books.Add(new Book(){ ISBN = "1", Title = "C#", Authors = new List<string>(){ "Anders", "Raja" }, Price=10, Publisher="Edictor1", Year = 2014 });
		Books.Add(new Book(){ ISBN = "2", Title = "Java", Authors = new List<string>(){ "Taua", "Tamas" }, Price=15, Publisher="Edictor2", Year = 2015 });
		Books.Add(new Book(){ ISBN = "3", Title = "JavaScript", Authors = new List<string>(){ "Istvan", "Ricardo" }, Price=20, Publisher="Edictor3", Year = 2016 });
		Books.Add(new Book(){ ISBN = "4", Title = "Perl", Authors = new List<string>(){ "Andre", "Andras" }, Price=25, Publisher="Edictor4", Year = 2017 });
		Books.Add(new Book(){ ISBN = "5", Title = "Python", Authors = new List<string>(){ "Jaqueline", "Nagy" }, Price=30, Publisher="Edictor5", Year = 2018 });
		
		Members.Add(new Member(){ FirstName = "Gerold", LastName = "Nagy", DateOfJoining = new DateTime(2018,12,25), DateOfBirth = new DateTime(1990,12,25) });
		Members.Add(new Member(){ FirstName = "Balint", LastName = "Tony", DateOfJoining = new DateTime(2018,10,20), DateOfBirth = new DateTime(1991,10,26) });		
		
		JournalEntry entry1 = new JournalEntry(10) { Member = Members[0], Books = new List<Book>(){ Books[1], Books[3] }};
		JournalEntry entry2 = new JournalEntry(10) { Member = Members[1], Books = new List<Book>(){ Books[2], Books[4] }};
		
		entry1.CheckIn(DateTime.Now.AddDays(9));
		entry2.CheckIn(DateTime.Now.AddDays(12));
		
		Journal journal = new Journal();
		journal.AddJournalEntry(entry1);
		journal.AddJournalEntry(entry2);
		journal.PrintMembersWhoAreLate();
		journal.PrintLateFeesForAllMembers();
	}
}

public class Journal{
	public List<JournalEntry> JournalEntries = new List<JournalEntry>();
	public void AddJournalEntry(JournalEntry journalEntry)
	{
		JournalEntries.Add(journalEntry);
	}
	
	private List<JournalEntry> JournalEntriesLate()
	{
		return 	JournalEntries.Where(je => je.ActualCheckinDate > je.DueDate).ToList();
	}
	
	public void PrintMembersWhoAreLate()
	{
		//{0} was late, ActualCheckinDatee: {1}, DueDate: {2}
		
		JournalEntriesLate().ForEach(je => {
			Console.WriteLine("{0} was late, ActualCheckinDatee: {1}, DueDate: {2}", je.Member.FullName, je.ActualCheckinDate, je.DueDate);
		});
	}
	
	public void PrintLateFeesForAllMembers()
	{
		//0.5Dolar
		List<JournalEntry> listFee = JournalEntriesLate();
		listFee.ForEach(item =>{
			//how many days
			TimeSpan daysFee = item.ActualCheckinDate.Subtract(item.DueDate);
			double daysTax = daysFee.TotalDays * 0.5;			
			Console.WriteLine(item.Member.FullName + " Fee it's $" + daysTax.ToString());
		});
	}
}

public class JournalEntry{
	public JournalEntry(int Days){
		DueDate = DateTime.Now.AddDays(Days);
		CheckoutDate = DateTime.Now;
	}
	
	public void CheckIn(DateTime actualCheckinDate)
	{
		ActualCheckinDate = actualCheckinDate;
	}
	
	public Member Member {get;set;}
	public DateTime ActualCheckinDate {get;set;}
	public DateTime DueDate {get;set;}
	public DateTime CheckoutDate {get;set;}
	public List<Book> Books {get;set;}
}

public class Book{
	//ISBN, Title, Authors, Price, Publisher, Year
	public string ISBN {get; set;}
	public string Title {get; set;}
	public List<string> Authors {get; set;}
	public decimal Price {get;set;}
	public string Publisher {get;set;}
	public int Year {get;set;}
}

public class Member{
	//LastName, FirstName, DateOfJoining, DateOfBirth
	public string LastName {get;set;}
	public string FirstName {get;set;}
	public string FullName { get {return FirstName + " " + LastName; } }	
	public DateTime DateOfJoining {get;set;}
	public DateTime DateOfBirth {get;set;}
}