import { Table, Button, Space, notification, Affix } from "antd";
import { useState, useEffect } from "react";
import Passenger from "../components/passengerDetails/passenger";
import "../../app/globals.css";
import { api } from "../core/api";
import ReservationModal from "../components/modals/reservationModal";
import PassengerModal from "../components/modals/passengerModal";
import EditReservationModal from "../components/modals/editReservationModal";
import Cruise from "../components/cruiseInfo/cruise";
import LocationModal from "../components/modals/locationModal";

function Krstarenja1() {
  const [krstarenja, setKrstarenja] = useState([]);
  const [modalPassengerVisible, setModalPassengerVisible] = useState(false);
  const [modalReservationVisible, setModalReservationVisible] = useState(false);
  const [modalCruisingVisible, setModalCruisingVisible] = useState(false);
  const [modalLocationVisible, setModalLocationVisible] = useState(false);
  const [currentReservationUpdating, setCurrentReservationUpdating] =
    useState();
  const [refetch, setRefetch] = useState(false);
  const [currentCruise, setCurrentCruise] = useState(1);

  async function handleReservationDelete(reservationId) {
    const response = await api.delete("/api/Rezervacija/" + reservationId);
    if (response.status == 204) {
      notification.open({
        message: "Rezervacija obrisana!",
      });
      setRefetch((prev) => !prev);
    } else {
      notification.open({
        message: "Dogodila se pogreška, pokušajte ponovno!",
      });
    }
  }

  const columns = [
    {
      title: "ID",
      dataIndex: "id",
      key: "id",
    },
    {
      title: "Datum rezervacije",
      dataIndex: "vrijeme",
      key: "vrijeme",
    },
    {
      title: "Broj putnika",
      dataIndex: "brojputnika",
      key: "brojputnika",
    },
    {
      title: "",
      key: "operation",
      render: (record) => (
        <a
          onClick={() => {
            console.log("key ", record);
            setCurrentReservationUpdating(record);
            setModalCruisingVisible(true);
          }}
        >
          Uredi
        </a>
      ),
    },
    {
      title: "",
      key: "operation",
      render: (record) => (
        <a
          onClick={() => {
            handleReservationDelete(record.id);
          }}
        >
          Obriši
        </a>
      ),
    },
  ];

  useEffect(() => {
    api
      .get("/api/Krstarenje/GetAll")
      .then((response) => {
        console.log(response.data);
        setKrstarenja(response.data);
      })
      .catch((error) => {
        console.error(error);
      });
  }, [refetch]);

  return (
    <>
      <Cruise
        cruise={krstarenja[currentCruise]}
        setCurrentCruise={setCurrentCruise}
        numberOfCruises={krstarenja.length}
        setRefetch={setRefetch}
      />
      <div className="m-5 flex justify-end">
        <Button type="primary" onClick={() => setModalReservationVisible(true)}>
          Kreiraj rezervaciju
        </Button>
      </div>
      <Table
        style={{
          margin: "20px",
        }}
        columns={columns}
        expandable={{
          expandedRowRender: (record) => (
            <p
              style={{
                margin: 0,
                width: "40%",
                paddingLeft: "50px",
              }}
            >
              <Passenger passenger={record.putnik} />
            </p>
          ),
        }}
        dataSource={krstarenja[currentCruise]?.rezervacije}
      />
      <div className="m-5">
        <Affix offsetBottom={10}>
          <Space>
            <Button
              type="primary"
              onClick={() => setModalPassengerVisible(true)}
            >
              Kreiraj putnika
            </Button>
            <Button
              type="primary"
              onClick={() => setModalLocationVisible(true)}
            >
              Dodaj/Obriši lokacije
            </Button>
          </Space>
        </Affix>
      </div>
      <ReservationModal
        visible={modalReservationVisible}
        setVisible={setModalReservationVisible}
        krstarenjeId={krstarenja[currentCruise]?.id}
        setRefetch={setRefetch}
      />
      <PassengerModal
        visible={modalPassengerVisible}
        setVisible={setModalPassengerVisible}
      />
      <EditReservationModal
        visible={modalCruisingVisible}
        setVisible={setModalCruisingVisible}
        reservation={currentReservationUpdating}
        setRefetch={setRefetch}
      />
      <LocationModal
        visible={modalLocationVisible}
        setVisible={setModalLocationVisible}
      />
    </>
  );
}

export default Krstarenja1;
