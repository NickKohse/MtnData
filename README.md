# MtnData
A web based platform to track trip reports on alpine trails.

## Version History
```
1.0 Alpha - A basic framework on a front end to test backend functionality, security features and better ui will follow - in development
```
## TODO
- Finish login/created view
- Change login to the username of the logged in user in the navbar on login
- deal with possible nulls for inserting location
- fix repetition in add/update sql functions(fixed?)
- recover from deleting multiple rows in delete location function
- change modify attribute function to be more like modify location function, with extra complication of non changable attributes for user
- think of better names for login and location actions
- find a way to un-greyout verified checkbox if an admin is logged in
- better error checking on client side for new location page(done?)
- get rid of unused using's in all files
- find a better way to handle successful creation of a new location/user
- make region selection available from drop down list on add location page
- make the location search method search for values in other columns
- proofread comments
- Create single format for writing error/exception messages
- Find a way to show results of searching a location
- Make a page for each location
- create a cache of locations for the user to keep in memory for the session (10-20 locations)
- give the option to add e new location if its not found in the database
- fix casting issue in location search ***
- make a decision between peakev and finalev
- fix rounding when displaying distance
- Add ability to add comments on a location
- Add a way for the location page to render the comments
- Make sure the show location page doesn't render the full page if there is no location
- Change DBConnect inheritance so none of the sub classes have ctors since they don't do anything
- Find a better location for GoodPassword function
- Finish function comments
- Check in add comment function to make sure referential integrity is intact before adding