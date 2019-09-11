<?php


namespace App\Controller;


use App\Events\RoomAddedToInventory;
use App\EventStore\EventStore;
use App\ReadModels\RoomExists;
use DateTimeImmutable;
use Exception;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;

class CreateRoomController extends AbstractController
{
    /**
     * @var EventStore
     */
    private $eventStore;

    /**
     * @var RoomExists
     */
    private $roomExists;

    public function __construct(EventStore $eventStore, RoomExists $roomExists)
    {
        $this->eventStore = $eventStore;
        $this->roomExists = $roomExists;
    }

    public function __invoke(Request $request): Response
    {
        $id = $request->request->getInt('id');
        $type = $request->request->getAlnum('type');

        if (($this->roomExists)($id)) {
            throw new Exception("Room #$id already exists");
        }

        $this->eventStore->append(new RoomAddedToInventory($id, $type, new DateTimeImmutable()), "room$id");

        return $this->redirectToRoute('room_inventory_dashboard');
    }
}
