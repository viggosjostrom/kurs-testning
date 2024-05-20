import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given('that I am on the product page', () => {
  cy.visit('/products');
});

When('I choose the category {string}', (category) => {
  cy.get('#categories').select(category);
});

Then('I should see the product {string} with price {string} and description {string}', (productName, price, description) => {
  cy.contains('.product .name', productName).should('be.visible');
  cy.contains('.product .name', productName)
    .siblings('.price').should('contain', price);

  cy.contains('.product .name', productName)
    .siblings('.description').should('be.visible')
    .and('contain', description);
});


Then('I should not see the product {string}', (productName) => {
  cy.get('.product .name').should('not.contain', productName);
});

Then('I should see all products with correct prices and descriptions', () => {
  const products = [
    { name: 'Ettan lös', price: '45', description: 'Lössnus från märket Ettan.' },
    { name: 'Velo Mint Xtreme-strong', price: '55', description: 'Velos dundersnus. Deras starkaste variant.' },
    { name: 'Siberia Portion', price: '70', description: 'Portionssnus från märket Siberia. 1000 mg nikotin per prilla.' },
    { name: 'Mormors chokladbollar', price: '3', description: 'Mormors äkta chokladbollar, hemmabakade.' },
    { name: 'Farbror Konrads kåldolmar', price: '10', description: 'Läckra sockrade kåldolmar, från Konrads Bageri.' },
    { name: 'Menaids Magiska Multidryck', price: '5', description: 'Menaids magiskt goda multidryck, bryggd i Åstorp.' },
    { name: 'Bennys Bengaliska Bubblor', price: '15', description: 'Bubbligare läsk går ej att hitta. Dricks på egen risk.' },
    { name: 'Dans Dansande Degknyten', price: '9', description: 'Degknyten som verkligen får magen att dansa, på nolltid!' },
    { name: 'Klas Kluriga Kombuchaläsk', price: '8', description: 'Kombucha så klurig att dess existens inte går att förstå.' },
    { name: 'Erics Erfarna Ekoläsk', price: '2', description: 'Halvt äkta läsk lagrad på oljefat' },
    { name: 'Blandad kompost.', price: '1', description: 'Ruttet kompostavfall.' },
    { name: 'Miljöfarligt avfall.', price: '99', description: 'Restprodukter och biprodukter av blandat slag.' },
    { name: 'Marabou Stenbitsrom', price: '12', description: 'Läckerheter från havet möter mmm marabou.' },
    { name: 'Marabou Jul', price: '12', description: 'Choklad med smak av julen.' },
    { name: 'Marabou Lök', price: '12', description: 'Lökig choklad.' },
  ];

  products.forEach(product => {
    cy.contains('.product .name', product.name).should('be.visible');
    cy.contains('.product .name', product.name)
      .siblings('.price').should('contain', product.price);
    cy.contains('.product .name', product.name)
      .siblings('.description').should('contain', product.description);
  });
});
