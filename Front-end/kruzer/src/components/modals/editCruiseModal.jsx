import { Select, Modal, Form, Input, notification, DatePicker } from "antd";
import { useState, useEffect } from "react";
import dayjs from "dayjs";
import { api } from "@/src/core/api";
const { TextArea } = Input;

function EditCruiseModal({ visible, setVisible, setRefetch, cruise }) {
  const {
    id,
    datumpocetak: datumpocetakOld,
    datumkraj: datumkrajOld,
    lokacije: lokacijeOld,
    naslov,
    opis,
  } = cruise;

  console.log("cruise ", cruise, lokacijeOld);

  const [lokacijeState, setLokacije] = useState([]);
  const [datumpocetakState, setDatumPocetak] = useState(datumpocetakOld);
  const [datumkrajState, setDatumKraj] = useState(datumkrajOld);
  const [defLoc, setDefLoc] = useState([]);

  useEffect(() => {
    console.log("datumpocetakOld ", datumpocetakOld, datumkrajOld);
    const arrayOfDefaultLocation = lokacijeOld?.map((lokacija) => ({
      value: lokacija.id,
      label: lokacija.grad + " (" + lokacija.država + ")",
    }));
    setDefLoc(arrayOfDefaultLocation);
    api
      .get("/api/Lokacija/GetAll")
      .then((response) => {
        console.log(response.data);
        const data = response.data.map((lokacija) => ({
          value: lokacija.id,
          label: lokacija.grad + " (" + lokacija.država + ")",
        }));
        setLokacije(data);
        form.setFieldValue(
          "lokacijeIds",
          data.map((lokacija) => lokacija.value)
        );
      })
      .catch((error) => {
        console.error(error);
      });
  }, []);

  const handleCancel = () => {
    setVisible(false);
  };

  const handleLocationChange = (option) => {
    //console.log("options ", option);
    form.setFieldValue("lokacijeIds", option);
  };

  async function handleOk() {
    let data = form.getFieldsValue();

    if (!data.datumpocetak) data.datumpocetak = datumpocetakState;
    if (!data.datumkraj) data.datumkraj = datumkrajState;

    if (!data.lokacijeIds) {
      data.lokacijeIds = [];
    }
    data.id = id;
    console.log(data);
    const response = api
      .put("/api/Krstarenje/" + id, data)
      .then((res) => {
        notification.open({
          message: "Krstarenje ažurirano!",
        });
        setRefetch((prev) => !prev);
      })
      .catch((error) => {
        notification.open({
          message: "Dogodila se pogreška, pokušajte ponovno!",
        });
      });
    form.resetFields();

    /*if (response.status == 204) {
      notification.open({
        message: "Krstarenje ažurirano!",
      });
      setRefetch((prev) => !prev);
    } else {
      notification.open({
        message: "Dogodila se pogreška, pokušajte ponovno!",
      });
    }*/
    setVisible(false);
  }

  function onDateStartChange(date, dateString) {
    //console.log("date start ", date, dateString);
    //form.setFieldValue("datumpocetak", dateString);
    setDatumPocetak(dateString);
    console.log(form.getFieldsValue());
  }

  function onDateEndChange(date, dateString) {
    //console.log("date end ", date, dateString);
    //form.setFieldValue("datumkraj", dateString);
    setDatumKraj(dateString);
  }

  const [form] = Form.useForm();

  return (
    <>
      <Modal
        title="Uredi krstarenje"
        open={visible}
        onOk={handleOk}
        onCancel={handleCancel}
        okText="Pošalji"
        cancelText="Natrag"
      >
        <Form
          form={form}
          layout="vertical"
          autoComplete="off"
          name="validateOnly"
        >
          <Form.Item
            name="naslov"
            label="Naslov"
            rules={[
              {
                required: true,
              },
            ]}
            initialValue={naslov}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="opis"
            label="Opis"
            rules={[
              {
                required: true,
              },
            ]}
            initialValue={opis}
          >
            <TextArea />
          </Form.Item>
          <Form.Item
            name="datumpocetak"
            label="Datum početka"
            rules={[
              {
                required: true,
              },
            ]}
          >
            <DatePicker
              onChange={onDateStartChange}
              format="YYYY-MM-DD"
              defaultValue={dayjs(datumpocetakOld, "YYYY-MM-DD")}
            />
          </Form.Item>
          <Form.Item
            name="datumkraj"
            label="Datum završetka"
            rules={[
              {
                required: true,
              },
            ]}
          >
            <DatePicker
              onChange={onDateEndChange}
              format="YYYY-MM-DD"
              defaultValue={dayjs(datumkrajOld, "YYYY-MM-DD")}
            />
          </Form.Item>

          <Form.Item
            name="lokacijeIds"
            label="Lokacije"
            rules={[
              {
                required: true,
              },
            ]}
          >
            <Select
              defaultValue={defLoc}
              placeholder="Odaberi lokacije"
              mode="multiple"
              onChange={handleLocationChange}
              options={lokacijeState}
            />
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
}
export default EditCruiseModal;
