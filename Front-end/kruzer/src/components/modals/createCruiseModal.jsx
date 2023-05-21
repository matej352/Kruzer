import { Select, Modal, Form, Input, notification, DatePicker } from "antd";
import { useState, useEffect } from "react";
import { api } from "@/src/core/api";
const { TextArea } = Input;
import moment from "moment";

function CreateCruiseModal({ visible, setVisible, setRefetch }) {
  const [lokacije, setLokacije] = useState([]);
  const [datumpocetak, setDatumPocetak] = useState();
  const [datumkraj, setDatumKraj] = useState();

  useEffect(() => {
    api
      .get("/api/Lokacija/GetAll")
      .then((response) => {
        console.log(response.data);
        const data = response.data.map((lokacija) => ({
          value: lokacija.id,
          label: lokacija.grad + " (" + lokacija.država + ")",
        }));
        setLokacije(data);
      })
      .catch((error) => {
        console.error(error);
      });
  }, []);

  const handleCancel = () => {
    setVisible(false);
  };

  const handleLocationChange = (option) => {
    console.log("options ", option);
    form.setFieldValue("lokacijeIds", option);
  };

  async function handleOk() {
    await form.validateFields();
    //form.setFieldValue("datumpocetak", datumpocetak);
    //form.setFieldValue("datumkraj", datumkraj);
    const data = form.getFieldsValue();
    if (!data.lokacijeIds) {
      data.lokacijeIds = [];
    }
    data.datumpocetak = datumpocetak;
    data.datumkraj = datumkraj;
    const response = api
      .post("/api/Krstarenje/Create", data)
      .then((res) => {
        notification.open({
          message: "Krstarenje kreirano!",
        });
        setRefetch((prev) => !prev);
      })
      .catch((error) => {
        notification.open({
          message: "Dogodila se pogreška, pokušajte ponovno!",
        });
      });
    form.resetFields();

    /*if (response.status == 204 || response.status == 200) {
      notification.open({
        message: "Krstarenje kreirano!",
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

  const validateStartDate = (_, value) => {
    if (value && value.isBefore(moment().startOf("day"))) {
      return Promise.reject(
        new Error("Datum početka ne može biti prije današnjeg datuma.")
      );
    }
    return Promise.resolve();
  };

  const validateEndDate = (_, value) => {
    const startDate = form.getFieldValue("datumpocetak");
    console.log(
      "adsjkladjkls ",
      startDate && value && moment(value).isBefore(startDate, "day"),
      startDate,
      value,
      moment(value).isBefore(startDate, "day")
    );
    if (startDate && value && moment(value).isBefore(startDate, "day")) {
      return Promise.reject(
        new Error("Datum završetka mora biti poslije datuma početka.")
      );
    }
    return Promise.resolve();
  };

  const [form] = Form.useForm();

  return (
    <>
      <Modal
        title="Novo krstarenje"
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
          >
            <TextArea />
          </Form.Item>
          <Form.Item
            name="datumpocetak"
            label="Datum početka"
            rules={[
              {
                required: true,
                validator: validateStartDate,
              },
            ]}
          >
            <DatePicker onChange={onDateStartChange} format="YYYY-MM-DD" />
          </Form.Item>
          <Form.Item
            name="datumkraj"
            label="Datum završetka"
            rules={[
              {
                required: true,
                validator: validateEndDate,
              },
            ]}
          >
            <DatePicker onChange={onDateEndChange} format="YYYY-MM-DD" />
          </Form.Item>
          <Form.Item
            name="kapacitet"
            label="Kapacitet"
            rules={[
              {
                required: true,
                validator: (_, value) => {
                  if (value <= 0) {
                    return Promise.reject(
                      new Error("Kapacitet mora biti veći od 0.")
                    );
                  }
                  return Promise.resolve();
                },
              },
            ]}
          >
            <Input type="number" />
          </Form.Item>
          <Form.Item
            name="popunjenost"
            label="Popunjenost"
            rules={[
              {
                required: true,
                validator: (_, value) => {
                  if (value <= 0) {
                    return Promise.reject(
                      new Error("Popunjenost mora biti veća od 0.")
                    );
                  }
                  return Promise.resolve();
                },
              },
            ]}
          >
            <Input type="number" />
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
              placeholder="Odaberi lokacije"
              mode="multiple"
              allowClear
              onChange={handleLocationChange}
              options={lokacije}
            />
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
}
export default CreateCruiseModal;
