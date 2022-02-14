# CoffeeShop (console)
1. ```git init```
2. ```git config core.sparsecheckout true```
3. Add CoffeeShop folder to info: ```echo CoffeeShop/ >> .git/info/sparse-checkout```
4. Add remote: ```git remote add -f git@github.com:front-end-2021/coffeeshop.git```
	- Output: will have ...
		* [new branch] dev  -> origin/dev
		* [new branch] main -> origin/main
5. Get files and folder: ```git checkout main```
6. ```git pull```

##### Note: sparse-checkout available +git v2.26.0