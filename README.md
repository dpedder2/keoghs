# Notes
Run `dotnet test` to run all tests. Please see KeoghsCheckout.Tests/BasketTests.cs for test cases.

## Requirements
- Can add item to basket
- Can add promotion to an item 
- Can get total cost of basket including any promotions present

## Constraints 
- For every multiple of 3 for item B, a promo of "3 for 40" is active for the 3 items
- For every multiple of 2 for item D purchased, a promo of "25% off" is active for the 2 items

# Ideas
## Classes
Item
LineItem
Promotion
Basket

## Notes
- Instead of hardcoded rules for a promo, allow them to be "added" for a SKU 
- When calculating total basket cost, separate out line items calculations if a promo is present
- Can't add more than one promo to a SKU
