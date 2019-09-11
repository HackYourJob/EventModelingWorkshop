FROM node:10.16.3-alpine as build

ADD . /build

WORKDIR /build

RUN npm install \
    && npm run build

FROM registry.access.redhat.com/rhscl/nginx-112-rhel7 as run

WORKDIR /opt/app-root/src

COPY --from=build /build/dist/ .

CMD /usr/libexec/s2i/run
