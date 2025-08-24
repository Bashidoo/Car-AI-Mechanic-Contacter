import Joi from 'joi';

export const loginSchema = Joi.object({
  email: Joi.string().email({ tlds: { allow: false } }).required().messages({
    'string.empty': 'E-post krävs',
    'any.required': 'E-post krävs',
    'string.email': 'Ange en giltig e-postadress',
  }),
  password: Joi.string().min(6).required().messages({
    'string.empty': 'Lösenord krävs',
    'any.required': 'Lösenord krävs',
    'string.min': 'Lösenordet måste vara minst 6 tecken',
  }),
});