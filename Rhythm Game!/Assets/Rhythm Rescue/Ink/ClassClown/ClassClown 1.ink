CONST name = "Name"

-> Start

== Start ==
{~What do you call a hippo who just fell over? A hipp-oh no! Hahaha!|The cloud behind you looks just like a {bee!|dog!|cat!|sheep!|tree!|cloud!}|Hey, it’s my best bud!|Is it me or do you like talking to me? I don’t blame you!} #happy

+[What's up?]
{~->Two|->Three|->Four|->Five|->One}

+ [Bye!]
-> Bye

== One ==
Hey, what's your favourite kind of music? #neutral

*[Pop music!]
-> Pop
*[Electronic]
-> Electronic
*[I prefer classical music.]
-> Classical

== Pop ==
Pop music is so fun to sing really loudly when no one's around. #happy
Sometimes I like to pretend someone is secretly watching me, thinking about how good I am at singing...and wants to sign me for a record deal! #happy
-> END
== Electronic ==
Nice choice, buddy! Nothing gets me fired up like a good beat to dance to! #happy
-> END
== Classical ==
Boooooring! Haha, just kidding, I like classical too. I am an animal of varied taste! #neutral
-> END


== Two ==
Y'know, I'm thinking about getting into stand-up comedy. What do you think? #happy
*[You'd be great!]
-> BeGreat
*[Eh...Don't quit your day job.]
->Eh
*[Can I hear a joke first?]
->HearAJoke

== BeGreat ==
I knew you'd say that! I'll get you a free ticket to my first gig, and you can sit right at the front! #neutral
{name}, my biggest fan! #happy
-> END

== Eh ==
Ha! That was a joke right? You should do comedy with me, that would be loads of fun, buddy! #happy
-> END

== HearAJoke ==
Oh cool, you want a preview of my show? Okay, here goes...Knock knock! #neutral
*[Who's there?]
->KnockKnock
*[Never mind.]
-> JudgeComedy

== KnockKnock ==
Hello, I'm here to talk about your car's extended warranty. #happy
-> JudgeComedy

== JudgeComedy ==
So, what do you think? #neutral
*[You should definitely do stand-up!]
->BeGreat
*[Hm, I'm not so sure. Maybe practice a bit more?]
->Practice

== Practice ==
Good idea, practice makes perfect right? Thanks for your help, buddy! #neutral
-> END

== Three ==

I used to be a bard in my hometown until I took to the countryside. #neutral

In fact, I could even sing a song for you right now, if you wanna hear! #neutral
*[I'd love to!]
-> SingSong
*[No thanks.]
-> NoSong

== SingSong ==
Here goes... #neutral
<i>Long ago in the deep blue sea, There was a shark with big sharp teeth, #neutral 
<i>But when you looked underneath, You could see he was friendly as can be! #happy
*[That was great!]
-> Great
*[Well that was...<i> something. </i>]
-> Something

== Great ==
Thanks! If you keep giving me compliments my head will get even bigger, buddy! #happy
-> END

== Something ==
Yeah, I gave it up because people weren't really listening anymore... #sad
-> END

== NoSong ==
Oh are you busy? Maybe another time then. I've gotta warm up my vocals anyway! #neutral
-> END

== Four ==
Don't let anyone say you can't do something, buddy. #Neutral
Unless it's something really dangerous, in which case, they might be right! #sad
-> END

== Five ==
It's a great day to hang out and do nothing! #happy
-> END

== Bye ==
{See you later, alligator!|Catch you in a bit, buddy!} #happy
-> END