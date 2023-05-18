import { Card, Typography, Button, Space, Col, Row } from "antd";
const { Title } = Typography;
import { useState } from "react";
import CreateCruiseModal from "../modals/createCruiseModal";
import EditCruiseModal from "../modals/editCruiseModal";

function Cruise({
  cruise = {},
  setCurrentCruise,
  numberOfCruises,
  setRefetch,
}) {
  const [visibleCreateCruise, setVisibleCreateCruise] = useState(false);
  const [visibleEditCruise, setVisibleEditCruise] = useState(false);

  const {
    datumkraj,
    datumpocetak,
    id,
    kapacitet,
    lokacije,
    naslov,
    opis,
    popunjenost,
  } = cruise;

  function handlePreviousCruise() {
    if (id == 1) return;
    else {
      setCurrentCruise((current) => current - 1);
    }
  }

  function handleNextCruise() {
    if (id == numberOfCruises) return;
    else {
      setCurrentCruise((current) => current + 1);
    }
  }

  return (
    <>
      <Card
        style={{
          margin: "20px",
        }}
      >
        <div className="flex justify-end">
          <Button type="primary" onClick={() => setVisibleCreateCruise(true)}>
            Novo krstarenje
          </Button>
        </div>
        <Row>
          <Col span={14}>
            <Title underline={true} level={2}>
              Krstarenje
            </Title>
            <Title level={4}>{naslov}</Title>
            <Title level={5}>{opis}</Title>

            <p>
              <strong>Trajanje putovanja:</strong> {datumpocetak}{" "}
              <strong>-</strong> {datumkraj}
            </p>
            <p>
              <strong>Destinacije: </strong>
              {lokacije?.map((lokacija, i) =>
                i == lokacije.length - 1 ? (
                  <span>
                    {lokacija.grad} ({lokacija.država}){""}
                  </span>
                ) : (
                  <span>
                    {lokacija.grad} ({lokacija.država}){",  "}
                  </span>
                )
              )}
            </p>
            <p>
              <strong>Maksimalni kapacitet:</strong> {kapacitet}
            </p>
            <p>
              <strong>Trenutna popunjenost:</strong> {popunjenost}
            </p>
            <a
              onClick={() => {
                setVisibleEditCruise(true);
              }}
            >
              Uredi krstarenje
            </a>
          </Col>
          <Col span={10} className="flex justify-end items-end">
            <div className="flex justify-end">
              <Space>
                <Button onClick={handlePreviousCruise}>{"<- "}Prethodno</Button>
                <Button onClick={handleNextCruise}>Iduće{" ->"}</Button>
              </Space>
            </div>
          </Col>
        </Row>
      </Card>
      <CreateCruiseModal
        visible={visibleCreateCruise}
        setVisible={setVisibleCreateCruise}
        setRefetch={setRefetch}
      />
      {visibleEditCruise && (
        <EditCruiseModal
          visible={visibleEditCruise}
          setVisible={setVisibleEditCruise}
          setRefetch={setRefetch}
          cruise={cruise}
        />
      )}
    </>
  );
}

export default Cruise;
