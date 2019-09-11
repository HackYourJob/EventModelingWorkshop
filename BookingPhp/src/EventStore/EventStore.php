<?php


namespace App\EventStore;


interface EventStore
{
    public function append(Event $event): void;

    public function getEvents(): array;
}