namespace Library_Management_Application
{
    class PremiumPatron : Patron
    {
        public string MembershipLevel { get; set; }
        public PremiumPatron() : base() { }
        public PremiumPatron(string name, int libraryCardNumber, string membershipLevel)
            : base(name, libraryCardNumber)
        {
            MembershipLevel = membershipLevel;
        }
        public void AccessVipLounge()
        {
            Console.WriteLine($"Premium Member {Name} has entered the VIP lounge.");
        }
        public override void BorrowBook(Book targetbook)
        {
            if (!targetbook.IsBorrowed)
            {
                targetbook.IsBorrowed = true;
                BorrowedBooks.Add(targetbook);
                Console.WriteLine($"Premium Member {Name} has borrowed '{targetbook.Title}'.");
            }
            else
            {
                Console.WriteLine($"Sorry, '{targetbook.Title}' is already borrowed.");
            }
        }
    }
}
