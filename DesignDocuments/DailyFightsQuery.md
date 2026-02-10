# Daily Fights Query For The Backend

Assuming we will use an SQL View, we will search through the RecentFights Table to check for if a fight was flagged to be a Daily Fight or not. This will give us all the recent daily fights, excluding any other non-daily fights within the recent fights table. 

Using a view removes the need to create another Table that may end up being redundent unless we find a need for one.