import Joi from 'joi';

export const registerSchema = Joi.object({
  firstName: Joi.string().required().messages({
    'string.empty': 'Namn krävs',
    'any.required': 'Namn krävs',
  }),
  lastName: Joi.string().required().messages({
    'string.empty': 'Efternamn krävs',
    'any.required': 'Efternamn krävs',
  }),
  email: Joi.string().email({ tlds: { allow: false } }).required().messages({
    'string.empty': 'E-post krävs',
    'any.required': 'E-post krävs',
    'string.email': 'Ange en giltig e-postadress',
  }),
  mobile: Joi.string()
    .pattern(/^(\+467[0-9]{8}|07[0-9]{8})$/)
    .required()
    .messages({
      'string.empty': 'Mobilnummer krävs',
      'any.required': 'Mobilnummer krävs',
      'string.pattern.base': 'Ange ett giltigt svenskt mobilnummer (t.ex. +46701234567 eller 0701234567)',
    }),
  address: Joi.string().required().messages({
    'string.empty': 'Adress krävs',
    'any.required': 'Adress krävs',
  }),
  postcode: Joi.string()
    .pattern(/^[0-9]{5}$/)
    .required()
    .messages({
      'string.empty': 'Postnummer krävs',
      'any.required': 'Postnummer krävs',
      'string.pattern.base': 'Ange ett giltigt postnummer (t.ex. 12345)',
    }),
  city: Joi.string().required().messages({
    'string.empty': 'Postort krävs',
    'any.required': 'Postort krävs',
  }),
  password: Joi.string().min(8).required().messages({
    'string.empty': 'Lösenord krävs',
    'any.required': 'Lösenord krävs',
    'string.min': 'Lösenordet måste vara minst 8 tecken',
  }),
  confirmPassword: Joi.string()
    .valid(Joi.ref('password'))
    .required()
    .messages({
      'string.empty': 'Bekräfta lösenord krävs',
      'any.required': 'Bekräfta lösenord krävs',
      'any.only': 'Lösenorden matchar inte',
    }),
});