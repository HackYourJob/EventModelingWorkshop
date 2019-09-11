<?php

namespace App\EventStore;

class JsonFileSystemEventStore implements EventStore
{

    /** @var string */
    private $directory;

    public function __construct(string $directory)
    {
        $this->directory = $directory;
        if(!is_dir($this->directory)) {
            throw new \InvalidArgumentException("[$directory] is NOT a directory");
        }
    }

    public function append(Event $event, string $suffix): void
    {
        $result = file_put_contents(
            sprintf(
                '%s/%s-%s-%s.json',
                $this->directory,
                $event->getDateTime()->format('U') * 1000,
                $event->getType(),
                $suffix
            ),
            json_encode($event->toJson())
        );

        if($result === false) {
            throw new \Exception("Failed to append event");
        }
    }

    public function getEvents(): array
    {
        $events = [];
        foreach(glob($this->directory.'/*.json') as $file) {
            $filename = basename($file, '.json');
            [$timestamp, $type] = explode('-', $filename);


            $events[] = Event::fromJson(
                $type,
                \DateTimeImmutable::createFromFormat('U', intdiv($timestamp, 1000)),
                json_decode(file_get_contents($file), true)
            );
        }

        return $events;
    }

}
