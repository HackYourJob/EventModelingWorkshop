FROM composer:latest AS composer
WORKDIR /code
COPY composer.json composer.lock symfony.lock ./
RUN composer install --prefer-dist --no-scripts --no-progress --no-suggest --no-interaction --ignore-platform-reqs


FROM php:7-apache AS web
WORKDIR /code
COPY ./ ./
COPY --from=composer /code/vendor /code/vendor
COPY ./apache.conf /etc/apache2/sites-available/000-default.conf

ARG APP_ENV=dev
ENV APP_ENV $APP_ENV
ARG APP_DEBUG=1
ENV APP_DEBUG $APP_DEBUG

RUN bin/console assets:install
RUN bin/console cache:warmup
