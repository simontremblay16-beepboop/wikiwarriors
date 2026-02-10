# Fight Selection Page Backend Design Document
The fight selection page backend should create an html page based off of the fight selection page frontend with a form for selecting two different Wikipedia pages that will fight. The fight selection page backend will need access to the “Fighters” Wikipedia page in order to display previews of the fighters that the user may select. Once the user has selected two legal fighters, they will be redirected to the fight depiction page.

[index.html] < ----------------------------------------
	|									              |
  Links to									          |
	|								            Links BACK to
    V								                  |
[Fight Selection Page Backend] --creates-- > [fightselection.html]
	|									              |
Connects with							            Links to
	|							               		  |
	V							            		  V
[DB Table: Fighters]     					 [fightdepiction.html]
										          

