# CoffeeShop (console)
1. ```git init```
2. ```git config core.sparsecheckout true```
3. Add CoffeeShop folder to info: ```echo CoffeeShop/ >> .git/info/sparse-checkout```
4. Add TestCoffeeShop folder to info: ```echo TestCoffeeShop/ >> .git/info/sparse-checkout```
5. Add remote: ```git remote add -f git@github.com:front-end-2021/coffeeshop.git```
	- Output: will have ...
		* [new branch] dev  -> origin/dev
		* [new branch] main -> origin/main
6. Get files and folder: ```git checkout main```
7. ```git pull```
8. Rightclick TestCoffeeShop project > Add > Project Reference...
	- ![Open menu](https://raw.githubusercontent.com/front-end-2021/coffeeshop/main/images/AddProjectReferenceToTestProject.jpg)
9. Select Browse... > Select file > Add
	- ![Select dll](https://raw.githubusercontent.com/front-end-2021/coffeeshop/main/images/AddProjectReferenceToTestProject%202.jpg)
10. Right click function and Run Test(s)
	- TestOrderWhiteCoffeeHotSmallAsync

##### Note: sparse-checkout available +git v2.26.0