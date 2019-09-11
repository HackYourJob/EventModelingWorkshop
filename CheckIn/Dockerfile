FROM python:3.7-alpine

RUN pip install -r requirements.txt

COPY . /home
WORKDIR /home

EXPOSE 5000

CMD env FLASK_APP=app.py flask run --host=0.0.0.0



