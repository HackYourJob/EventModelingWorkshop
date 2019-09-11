<?php

namespace App\EventStore;

use DateTimeImmutable;

class Event
{
    private $data = [];

    /**
     * @var string
     */
    private $type;

    /**
     * @var DateTimeImmutable
     */
    private $dateTime;

    public function __construct(string $type, array $data, DateTimeImmutable $dateTime)
    {
        $this->data = $data;
        $this->type = $type;
        $this->dateTime = $dateTime->format('U') * 1000;
    }

    public function toJson(): array
    {
        return $this->data;
    }

    static public function fromJson(string $type, DateTimeImmutable $dateTime, array $json): self
    {
        return new self($type, $json, $dateTime);
    }

    /**
     * @return string
     */
    public function getType(): string
    {
        return $this->type;
    }

    /**
     * @return DateTimeImmutable
     */
    public function getDateTime(): DateTimeImmutable
    {
        return DateTimeImmutable::createFromFormat('U', intdiv($this->dateTime, 1000));
    }

    /**
     * @return array
     */
    public function getData(): array
    {
        return $this->data;
    }
}
