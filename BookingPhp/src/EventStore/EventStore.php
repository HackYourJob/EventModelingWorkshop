<?php


namespace App\EventStore;


interface EventStore
{
    public function append(Event $event, string $suffix): void;

    public function getEvents(): array;
}