# MtnData
A web based platform to track trip reports on alpine trails.

## Version History
```
1.0 Alpha - A basic framework on a front end to test backend functionality, security features and better ui will follow - in development
```
## TODO
### For 1.0 Alpha
- Add trip report page / controller / db connector

### For Later
- find a way to un-greyout verified checkbox if an admin is logged in
- deal with possible nulls for inserting location
- recover from deleting multiple rows in delete location function
- change modify attribute function to be more like modify location function, with extra complication of non changable attributes for user
- get rid of unused using's in all files
- think of better names for login and location actions
- find a better way to handle successful creation of a new location/user\
- make region selection available from drop down list on add location page
- make the location search method search for values in other columns
- proofread comments
- Create single format for writing error/exception messages
- create a cache of locations for the user to keep in memory for the session (10-20 locations)
- give the option to add e new location if its not found in the database
- Finish function comments
- Make all local variables camel case including viewbag ones
- Put user in regular message payload, or make specialized message for each type
- Put functions in alphabetical order
- Delete extra spaces
- Find a better way to map db to objects
- Write unit tests
- Write end to end tests
- Finish the user page(show recent activity, allow password chasnge etc)
- Make sure username aren't the same if non csae sensitive
- Add Log rotation
- Switch to use async functions for db access
- Fix outstanding issues- Get rid of repetitive adding parameters in sql functions
- Get rid of repetitive stuff in 'select' sql functions











