public static class ExampleText {
    // https://www.irisclasson.com/2019/08/08/not-so-stupid-question-327-what-is-mqtt/ ?
    public static string BlogPost = @"The day started as always with breakfast, for me and baby Loke, before I biked to work. I’m training for my next triathlon and thus trying to get in as many miles on the bike as possible. The workday started with our daily standup, and a small discussion surrounding QA and end to end testing. We’ve been struggling to cope with the amount of testing that needs to be done as the product grows (mostly end-to-end testing and exploratory testing), and we decided two weeks ago to set aside three sprints to wire up proper end to end testing. The plan is as following:

Set up a separate test tenant with complex data (using the good old Adventureworks database) that we can run the following end-to-end tests on:
• Ghost Inspector GUI tests (our previously manually run exploratory tests will be recorded and automated)
• End to end testing of our API with Pester and PowerShell
• OpenAPI tests- once we have our API cleaned up

Yesterday I wrote a mock service that spits out mock data similar to the Ghost Inspector API result so we have some thing we can use when testing our scripts (as you have limited test runs with Ghost Inspector), and created a new project in Octopus Deploy (our deployment service) where we will run the different steps.

Today I wrote a Ghost Inspector PowerShell template for running tests against a suite. The template accepts two parameters, a suite URI and API key. Since I have the mock server, I’ve created the steps using the template and provided the URI for the mock service while testing the scripts.

The process starts with creating a backup of the test tenant database, and saving the backup with the release number as version number. Afterwards, the suite steps are run, outputting warnings if tests fail. A shared Octopus Deploy variable is incremented after each suite is run, setting the number of passed and failed tests.

At the end the tenant database is restored to its prior state, and the test result is outputted. If any of the tests failed, then the ‘deployment ‘fails.
The tests runs can be triggered manually, in addition to daily runs. Due to limited number of runs (cost issue) we can’t run them on every build.";

    public static string BookPreface = @"Many years ago, I started writing down funny stories, situations, and characters I came across as a programmer. I did this secretly, as I’m sure some of my higher uppers would have been less than thrilled about it, even though they sure talk a lot about the importance of documentation. To be fair, I merely documented the bizarre and humorous aspects of a job often considered mundane.
Time passed and in 2020, I had survived my first year as a first-time parent to a baby that hated sleep (and still does) and was awarded a day at the spa from my dear husband. As you do, when you get some much-needed rest, I used that time wisely and wrote an idea that would become the inspiration for this book. I essentially used my spa-day to work. Don’t judge me. This was when the lazy sub-par consultant Leo was born!  The stories and characters I had collected over the years became a part of Leo's life. After the needed day at the spa I went back home and realized I still had a bullfrog baby (bullfrogs don’t sleep) and the book, naturally, took longer to write than I had estimated. If you know anything about software developers, then you know we are the worst at estimating anything. And thus, the book was written with voice to text, on my phone, as I was walking for hours with my strong-personality child at night when he ought to be sleeping. This is great! I thought. And got myself another baby. Consequentially, the book was edited and rewritten during the frequent late-night feeding sessions newborns torture you with, while my dear husband wrangled the older one to sleep like a pro wrestler doing a final comeback in his (almost) forties. And through the parental haze of emotional masochism, I managed to put together a book I’m immensely proud of and I’m obviously going to become filthy rich.
All the situations, stories and characters are based on actual happenings, people, and stereotypes. Sleep deprivation was my fuel when caffeine wouldn’t do, and this passion project has taken me years to complete. Please make me rich. 
";

    public static List<string> BookReviews = new List<string>
{
    "This book arrived at my doorstep on a Friday…. I started reading it on Saturday morning and could not put it down all weekend! Fictional stories based on software development are very rare… and Iris has done a masterful job weaving together software industry concepts, tech stereotypes, product development trends, and … dentistry ;) A very humorous and witty read! I highly recommend this book to any IT professional.",
    "This is an incredibly fun read, that at times will have you sweating with anxiety, and wanting to drown out the cringe.You will laugh, you will double take.Great first book Iris!",
    "It's definitely very funny, with clearly anecdotes from the field. Has a couple of bugs (typos) here and there, but pretty enjoyable read.",
    "This is a genuinely funny book with quips that will make you laugh out loud at times! The main character is not afraid of self-mockery, and the story exudes this engaging sincerity, and an endearing vulnerability that makes me want to hear (READ) more about it. So, I do count on more!"
};

     public static string ChapterExample = @"
It was a unexpectedly warm and humid day in the middle of May and Leo was sweating profusely on the train heading towards the cathedral city of Peterborough located on the outskirts of London. Peterborough was a small city, and one could almost consider it a village, if it wasn’t for the damn cathedral. Everybody seemed to know about Peterborough, but for all the wrong reasons. It was a self-declared shithole, according to Leo, and The Sun. Voted the Biggest Dump in England in 2019, the best part about the city was the train station and the ability to leave quickly. 
".Replace(Environment.NewLine, " ");

    public static string ChapterExample2 = "if James gets to decide, then we’ll rewrite this until we retire and the robots take over, using a plethora of hipster libraries he heard about at a user group. It’ll be the most performant webshop in the world, but we’ll deploy on our deathbed ";
}
