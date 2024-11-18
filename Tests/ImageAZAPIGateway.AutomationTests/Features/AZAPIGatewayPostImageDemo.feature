Feature: AZ API Gateway post image demo

Scenario: Post image to Azure API Gateway success
	Given Go to API gateway portal
		And Open popup to try API post image
		And Input the API body <imageUrl> and <description>
	When Click Send button
	Then Verify the API response status is 200
	Then Verify image information <imageUrl> and <description> display on latestimageviewer page

	Examples:
	| imageUrl          | description					  |
	| https://image-$.png	| Image for demo AZ API Gateway - $	  |