import { Select, Modal, Form, Input, notification } from "antd";
import { useState, useEffect } from "react";
import { api } from "@/src/core/api";

function ReservationModal({
  visible,
  setVisible,
  krstarenjeId,
  setRefetch,
  krstarenje = {},
}) {
  const [putnici, setPutnici] = useState([]);

  useEffect(() => {
    api
      .get("/api/Putnik/GetAll")
      .then((response) => {
        console.log(response.data);
        const data = response.data.map((putnik) => ({
          value: putnik.id,
          label: putnik.ime + " " + putnik.prezime,
        }));
        setPutnici(data);
      })
      .catch((error) => {
        console.error(error);
      });
  }, []);

  const handleCancel = () => {
    setVisible(false);
  };

  const handleSelectChange = (option) => {
    form.setFieldValue("putnikId", option);
  };

  async function handleOk() {
    await form.validateFields();
    setVisible(false);
    const data = form.getFieldsValue();
    data.krstarenjeId = krstarenjeId;
    console.log("data ", data);
    const response = api
      .post("/api/Rezervacija/Create", data)
      .then((res) => {
        notification.open({
          message: "Rezervacija kreirana!",
        });
        setRefetch((prev) => !prev);
      })
      .catch((error) => {
        notification.open({
          message: "Dogodila se pogreška, pokušajte ponovno!",
        });
      });
    form.resetFields();

    /*if (response.status == 204 || response.status == 201) {
      notification.open({
        message: "Rezervacija kreirana!",
      });
      setRefetch((prev) => !prev);
    } else {
      notification.open({
        message: "Dogodila se pogreška, pokušajte ponovno!",
      });
    }*/
  }
  const validateBrojPutnika = (_, value) => {
    if (value && value <= 0) {
      return Promise.reject(new Error("Broj putnika mora biti barem 1"));
    }
    return Promise.resolve();
  };

  const [form] = Form.useForm();

  return (
    <>
      <Modal
        title="Nova rezervacija"
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
            name="putnikId"
            label="Putnik"
            rules={[
              {
                required: true,
              },
            ]}
          >
            <Select onChange={handleSelectChange} options={putnici} />
          </Form.Item>
          <Form.Item
            name="brojputnika"
            label="Broj putnika"
            rules={[
              {
                required: true,
                validator: validateBrojPutnika,
              },
            ]}
          >
            <Input type="number" />
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
}
export default ReservationModal;
