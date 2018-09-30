# MtnData
A web based platform to track trip reports on alpine trails.

## Version History
```
1.0 Alpha - A basic framework on a front end to test backend functionality, security features and better ui will follow - in development
```
## TODO
### For 1.0 Alpha
- Finish login/created view
- Change login to the username of the logged in user in the navbar on login
- fix repetition in add/update sql functions(fixed?)
- find a way to un-greyout verified checkbox if an admin is logged in
- better error checking on client side for new location page(done?)
- Find a way to show results of searching a location
- Make a page for each location(done?)
- fix casting issue in location search ***
- make a decision between peakev and finalev
- fix rounding when displaying distance(done?)
- Add ability to add comments on a location
- Add a way for the location page to render the comments
- Make sure the show location page doesn't render the full page if there is no location(irrelevant)
- Change DBConnect inheritance so none of the sub classes have ctors since they don't do anything(done?)
- Find a better location for GoodPassword function
- Check in add comment function to make sure referential integrity is intact before adding
- Remove unnecessary console writes 
- Why doesnt search location show message when there are no results
- Fix else of searchloc action result
- Fix problem showing comments
### For Later
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











