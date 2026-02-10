# Fight Depiction Page Backend Design Document
The fight depiction page backend should create an html page based off of the fight depiction page frontend. It will show an animation of our two “Fighters” dueling. The fight depiction page will access the database (Specifically the Fighters table) in order to retrieve the images associated with the wiki fighters. It composites those images onto the fighters to make then appear as the warriors for each wiki page. The fight depiction page will also need access to the fightVictoryEquation function in order to calculate the winner of the fight. Once a fight concludes, the fight depiction page will indicate the winner, then insert an entry into the “history” table of the Database with the required information about the fight. The page will also link back to the index page.

[index.html] < ---------------------------------------<----------------
    |                                                ^                |
  Links to                                           |                |
    |                                         Links BACK to           |
    V                                                |                |
[Fight Selection Page Backend] --creates-- > [fightselection.html]    |
    |                                                |                |
Connects with                                    Links to             |
    |                                                |                |
    V                                                |             Links
[DB Table: Fighters]                                 |             BACK to
    ^                                                |                |
    |                                                |                |
Connects with                                        |                |
    |                                                V                |
[Fight Depiction Backend] -- creates -- > [fightdepiction.html] -------
    |			    ^  ^
Connects with	    |  |
    |			    |   -----Returns data to -- [Wikipedia API Function]
    V			    |
[DB Table: History]  |
			    |
			Calculates
			    |
			    V
		[Fight Victory Equation]
