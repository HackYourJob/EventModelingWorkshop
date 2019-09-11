<?php

namespace App\Events;

use App\EventStore\Event;
use DateTimeImmutable;

class RoomAddedToInventory extends Event
{
    const eventType = 'roomAdded';

    public function __construct(int $roomId, string $roomType, DateTimeImmutable $dateTime)
    {
        parent::__construct(self::eventType.'-room'.$roomId, [
            'id' => $roomId,
            'type' => $roomType,
        ], $dateTime);
    }
}
