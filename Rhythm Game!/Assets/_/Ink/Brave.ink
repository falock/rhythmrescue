CONST name = "Name"

-> Start

== Start ==
{~Greetings, {name}!|It is a fine day today.|I am alert as ever!|A righteous day to you, {name}!}

*[What's up?]
{~->Two|->Three|->Four|->Five|->One}

* [Bye!]
-> Bye

== One ==
What is your favourite music genre?

*[Pop music!]
-> Pop
*[Electronic music. is more my style.]
-> Electronic
*[I prefer Classical music.]
-> Classical

== Pop ==
There is something for everyone within popular music. Of course, that is why it is popular!
-> END

== Electronic ==
Electronic music makes me feel like I am living in the future. It is intriguing.
-> END

== Classical ==
You have exquisite taste, my friend! 
-> END


== Two ==
What could've possiblty brainwashed 

*[Don't worry, we don't need to fight anyone to solve this. We just need the power of music!]
-> Peaceful
*When we find who's responsible, show them what for!
-> Aggressive

== Peaceful ==
I didn't take you for a weakling, {name}, but if you think that is the best course of action, I shall concede. Just say the word if you change your mind!
-> END

== Aggressive ==
That sounds like a good plan, {name}! They will face the consequences of their actions, as we all must.
-> END


== Three ==
I am training to be stronger, so I can better protect our team.

*Got any strength training tips?
-> StrengthTrainingTips
*You can relax while we're in the camp, we're safe here! 
-> SafeInCamp

== StrengthTrainingTips ==
{~What a wonderful question! |Thank you for asking! |Indeed I do. }{Always make sure you warm up beforehand, so your muscles are prepared!|You should rest for two days after strength training, so your muscles can heal.|Pay attention to your form, if it is not correct it could lead to an injury!}
-> END

== SafeInCamp ==
I refuse to relax. Even when the possibility of a surprise attack is low, it is never zero!
-> END


== Four ==
If you find yourself struggling, know that you can change the difficulty in your Journal. Or, if you are like myself and you are up for the challenge, you can just keep practicing until you succeed!
-> END


== Five ==
Remember, should you ever feel the need to change which buttons you press to hit notes, you can do so in the Settings section of your Journal!
-> END

== Bye ==
See you later, alligator!
-> END