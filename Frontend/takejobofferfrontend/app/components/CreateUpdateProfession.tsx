import { Input } from "antd";
import { ProfessionRequest } from "../services/professions";
import { useEffect, useState } from "react";
import TextArea from "antd/es/input/TextArea";
import Modal from "antd/es/modal/Modal";

export enum Mode {
  Create,
  Edit,
}

interface Props {
  mode: Mode;
  values: Profession;
  isModalOpen: boolean;
  handleCancel: () => void;
  handleCreate: (request: ProfessionRequest) => void;
  handleUpdate: (id: string, request: ProfessionRequest) => void;
}

export const CreateUpdateProfession = ({
  mode,
  values,
  isModalOpen,
  handleCancel,
  handleCreate,
  handleUpdate,
}: Props) => {
  const [name, setName] = useState<string>("");
  const [description, setDescription] = useState<string>("");

  useEffect(() => {
    setName(values.name), setDescription(values.description);
  }, [values]);

  const handleOnOk = async () => {
    const professionRequest = { name, description };

    mode == Mode.Create
      ? handleCreate(professionRequest)
      : handleUpdate(values.id, professionRequest);
  };

  return (
    <Modal
      title={
        mode === Mode.Create ? "Добавить профессию" : "Редактировать профессию"
      }
      open={isModalOpen}
      onOk={handleOnOk}
      onCancel={handleCancel}
      cancelText={"Отмена"}
    >
      <div className="profession__modal">
        <Input
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="Название"
        />
        <TextArea
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          autoSize={{ minRows: 3, maxRows: 3 }}
          placeholder="Описание"
        />
      </div>
    </Modal>
  );
};
