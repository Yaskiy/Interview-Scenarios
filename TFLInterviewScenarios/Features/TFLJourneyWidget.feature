Feature: TFLJourneyWidget

A short summary of the feature
Background: 
	Given that the application under test is successfully loaded
	And a user clicks on Accept only essential cookies

@tag1
Scenario: Test_01_Verify that a valid journey can be planned using the widget

	When a user input Grays Rail Station in the 'From' text-field on the Plan a journey widget
	And a user input London Victoria Train Station in the 'To' text-field 
	And the Leaving icon is set to now
	And a user clicks on plan my journey button
	Then the Journey results Page is Loaded with the Journey Information
	And the Journey Information contains the data below
	| FromLocation      | ToLocation       |
	| Transfer to Grays | Victoria Station |


Scenario: Test_02_Verify that the widget is unable to provide results when an invalid journey is planned
	
	When a user input Grays Rail Station in the 'From' text-field on the Plan a journey widget
	And a user input Oju in the 'To' text-field
	And a user clicks on plan my journey button
	Then the Journey results Page is Loaded with the Journey Information
	Then a Message Sorry, we can't find a journey matching your criteria should appear

Scenario: Test_03_Verify that the widget is unable to plan a journey if no locations are entered into the widget

		When a user clicks on plan my journey button
		Then the error messages below should appear
		| FromField                   | ToField                   |
		| The From field is required. | The To field is required. |

Scenario: Test_04_Verify change time link on the journey planner displays “Arriving” option and plan a journey based on arrival time

		When a user input Grays Rail Station in the 'From' text-field on the Plan a journey widget
		And a user input London Victoria Train Station in the 'To' text-field
		When a user clicks on the change time link
		Then the Arriving option must displayed
		When a user clicks on the Arriving option
		And user clicks on the Date dropdown and select Tomorrow
		And user clicks on the Time dropdown and select 14:30
		And a user clicks on plan my journey button
		Then the Journey results Page is Loaded with the Journey Information
		And the Journey Information contains the data below
		| FromLocation      | ToLocation       |
		| Transfer to Grays | Victoria Station |

Scenario:Test_05_Verify that a journey can be amended by using the “Edit Journey” button on the Journey results page

		When a user input Grays Rail Station in the 'From' text-field on the Plan a journey widget
		And a user input London Victoria Train Station in the 'To' text-field
		And a user clicks on plan my journey button
		Then the Journey results Page is Loaded with the Journey Information
		When a user clicks on the Edit Journey link	
		And a user re-input Dagheham Rail Station in the 'From' text-field on the Plan a journey widget
		And a user re-input London Victoria Train Station in the 'To' text-field
		And a user clicks on the Update Journey button
		Then a new Journey results Page is Loaded with a new Journey Information
		And the Journey Information contains the data below
		| FromLocation         | ToLocation       |
		| Transfer to Dagenham | Victoria Station |

Scenario:Test_06_Verify that the “Recents” tab on the widget displays a list of recently planned journeys

		When a user clicks on Recents tab
		And a user clicks on the Accept functionality cookies link
		And a user clicks on the accept all cookies button
		And a user clicks on the New tab
		And a user input Grays Rail Station in the 'From' text-field on the Plan a journey widget
		And a user input London Victoria Train Station in the 'To' text-field
		And a user clicks on plan my journey button
		Then the Journey results Page is Loaded with the Journey Information
		When a user clicks on the Edit Journey link	
		And a user re-input Dagheham Rail Station in the 'From' text-field on the Plan a journey widget
		And a user re-input London Victoria Train Station in the 'To' text-field
		And a user clicks on the Update Journey button
		Then a new Journey results Page is Loaded with a new Journey Information
		And the Journey Information contains the data below
		| FromLocation         | ToLocation       |
		| Transfer to Dagenham | Victoria Station |
		When a user clicks on the plan a journey button
		And a user clicks on Recents tab
		Then the following journeys should appear under Recents tab
		| Recent1                                                | Recent2                                             |
		| Dagheham Rail Station to London Victoria Train Station | Grays Rail Station to London Victoria Train Station |
		                                                     
